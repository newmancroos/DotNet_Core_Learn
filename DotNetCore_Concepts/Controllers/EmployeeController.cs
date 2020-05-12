using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DotNetCore_Concepts.Dtos;
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
        private readonly IMapper _mapper;
        public EmployeeController(IRepository<Employee> employeeRepo, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("get/{id}", Name = "getEmployee")]
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

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> CreateEmployee([FromBody]EmployeeDto employee)
        {
            try
            {
                var emp = _mapper.Map<Employee>(employee);
                _employeeRepo.Add(emp);
                await _employeeRepo.SaveChangesAsync();
                return CreatedAtRoute("getEmployee", new { id = emp.Id },emp);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}