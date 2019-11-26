using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Net_MVC.Models;
using Asp_Net_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Net_MVC.Controllers
{
    //[Route("Home")]
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        public readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("Index")]
        //[Route("~/")]  // this makes this method is a root default method so we can give http://Localhost:44340/ will display this view
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
        public ViewResult Details(int? id)
        {
            //var employee = _employeeRepository.GetEmpoyee(1);
            ////We can call a view other than view which same as method name
            //return View("Test", employee);

            //View can also have our view anywhere and specify the absolute path when call the view
            //return View("Myviews/Test.cshtml");

            //return View(employee);

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmpoyee(id ?? 1),
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

        [HttpPost]
        //public RedirectToActionResult Create(Employee employee)
        public IActionResult Create(Employee employee)
        {
            if(ModelState.IsValid)
            { 
                Employee newEmployee =  _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }
            return View();
        }
    }
}