using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Net_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Net_MVC.Controllers
{
    public class HomeController : Controller
    {
        public readonly IEmployeeRepository _employeeRepository;
        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public string Index()
        {
           return _employeeRepository.GetEmpoyee(1).Name;
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
        public ViewResult Details()
        {
            var employee = _employeeRepository.GetEmpoyee(1);
            ////We can call a view other than view which same as method name
            //return View("Test", employee);

            //View can also have our view anywhere and specify the absolute path when call the view
            //return View("Myviews/Test.cshtml");

            return View(employee);
        }

        public ViewResult UsingViewData()
        {
            var employee = _employeeRepository.GetEmpoyee(1);
            ViewData["Employee"] = employee;
            ViewData["PageTitle"] = "Employee Details";
            return View();
        }

        public ViewResult UsingViewBag()
        {
            var employee = _employeeRepository.GetEmpoyee(1);
            ViewBag.Employee = employee;
            ViewBag.PageTitle = "Employee Details";
            return View();
        }
        public ViewResult UsingStronglyType()
        {
            var employee = _employeeRepository.GetEmpoyee(1);
            ViewBag.PageTitle = "Employee Details";
            return View(employee);
        }

    }
}