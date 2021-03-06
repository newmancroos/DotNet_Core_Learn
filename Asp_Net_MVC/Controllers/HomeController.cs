﻿using System;
using System.IO;
using Asp_Net_MVC.Models;
using Asp_Net_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asp_Net_MVC.Controllers
{
    [Authorize]
    //[Route("Home")]
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public readonly IEmployeeRepository _employeeRepository;
        //private readonly IHostingEnvironment _hostingEnvironment;
        //IHostingEnvironment is absolute need to to alternative
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment, ILogger<HomeController> logger)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Index")]
        //[Route("~/")]  // this makes this method is a root default method so we can give http://Localhost:44340/ will display this view
        //[Authorize]
        [AllowAnonymous]
        public ViewResult Index()
        {
            //return _employeeRepository.GetEmpoyee(1).Name;
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        //public JsonResult Details()
        //{
        //    var employee = _employeeRepository.GetEmpoyee(1);
        //    return Json(employee);
        //}
        //public ObjectResult Details()
        //{
        //    var employee = _employeeRepository.GetEmpoyee(1);
        //    return Ok(employee);
        //}
        //public ObjectResult Details()
        //{
        //    var employee = _employeeRepository.GetEmpoyee(1);
        //    return new ObjectResult(employee);
        //}
        //Since it is Mvc application we need to return view
        //[Route("Home/Details")]
        //[Route("Details")]
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            //var employee = _employeeRepository.GetEmpoyee(1);
            ////We can call a view other than view which same as method name
            //return View("Test", employee);

            //View can also have our view anywhere and specify the absolute path when call the view
            //return View("Myviews/Test.cshtml");

            //return View(employee);

            //throw new Exception("Exception Occured");   //Testing exception and logging
            _logger.LogTrace("From Home Controller - Log Trace");
            _logger.LogDebug("From Home Controller - Log Debug");
            _logger.LogInformation("From Home Controller - Log Information");
            _logger.LogWarning("From Home Controller - Log Warning");
            _logger.LogError("From Home Controller - Log Error");
            _logger.LogCritical("From Home Controller - Critical Log");
            Employee employee = _employeeRepository.GetEmpoyee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "EmployeeDetails"
            };
            return View(homeDetailsViewModel);
        }
        //[Route("Home/UsingViewData")]
        //[Route("UsingViewData")]
        public ViewResult UsingViewData()
        {
            var employee = _employeeRepository.GetEmpoyee(1);
            ViewData["Employee"] = employee;
            ViewData["PageTitle"] = "Employee Details";
            return View();
        }

        //[Route("Home/UsingViewBag")]
        //[Route("UsingViewBag")]
        public ViewResult UsingViewBag()
        {
            var employee = _employeeRepository.GetEmpoyee(1);
            ViewBag.Employee = employee;
            ViewBag.PageTitle = "Employee Details";
            return View();
        }
        //[Route("Home/UsingStronglyType/{id?}")]
        //[Route("UsingStronglyType/{id?}")]
        //[Route("[controller]/[action]/{id?}")]
        public ViewResult UsingStronglyType(int? id)
        {
            //var employee = _employeeRepository.GetEmpoyee(1);
            //ViewBag.PageTitle = "Employee Details";
            /////ViewModel Sample
            //return View(employee);

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmpoyee(id ?? 1),
                PageTitle = "EmployeeDetails"
            };
            return View(homeDetailsViewModel);
        }
        
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //[HttpPost]
        ////public RedirectToActionResult Create(Employee employee)
        //public IActionResult Create(Employee employee)
        //{
        //    if(ModelState.IsValid)
        //    { 
        //        Employee newEmployee =  _employeeRepository.Add(employee);
        //        return RedirectToAction("details", new { id = newEmployee.Id });
        //    }
        //    return View();
        //}

        [HttpPost]
        //public RedirectToActionResult Create(Employee employee)
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //string uniqueFileName = null;
                //if (model.Photo != null)
                //{
                //    string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                //    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName.Substring(model.Photo.FileName.LastIndexOf("\\") + 1);
                //    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                //    model.Photo.CopyTo(new FileStream(filePath,FileMode.Create));
                //}
                string uniqueFileName = ProcessUploadedFile(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                Employee emp =  _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmpoyee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        //[HttpPut]
        //public IActionResult Edit(EmployeeEditViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string uniqueFileName = null;
        //        if (model.Photo != null)
        //        {
        //            string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        //            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName.Substring(model.Photo.FileName.LastIndexOf("\\") + 1);
        //            string filePath = Path.Combine(uploadFolder, uniqueFileName);
        //            model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
        //        }
        //        Employee newEmployee = new Employee
        //        {
        //            Name = model.Name,
        //            Email = model.Email,
        //            Department = model.Department,
        //            PhotoPath = uniqueFileName
        //        };
        //        Employee emp = _employeeRepository.Add(newEmployee);
        //        return RedirectToAction("details", new { id = newEmployee.Id });
        //    }
        //    return View();
        //}
        [HttpPost]
        //public RedirectToActionResult Create(Employee employee)
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmpoyee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;

                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);

                }

                Employee emp = _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName.Substring(model.Photo.FileName.LastIndexOf("\\") + 1);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult FullCalendar()
        {
            return View();
        }
    }
}