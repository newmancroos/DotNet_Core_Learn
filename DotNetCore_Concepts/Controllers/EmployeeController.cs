using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_Concepts.Entities;
using DotNetCore_Concepts.Repoitories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Concepts.Controllers
{
    [Route("employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepo;
        public EmployeeController(IRepository<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeRepo.GetAsync(id);
        }

        [HttpGet]
        [Route("getall")]
        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _employeeRepo.GetAllAsync();
        }
    }
}