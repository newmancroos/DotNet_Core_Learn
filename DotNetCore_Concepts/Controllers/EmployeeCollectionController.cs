using AutoMapper;
using DotNetCore_Concepts.Dtos;
using DotNetCore_Concepts.Entities;
using DotNetCore_Concepts.Repoitories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore_Concepts.Controllers
{
    [Route("empCollection")]
    [ApiController]
    public class EmployeeCollectionController : ControllerBase
    {
        private readonly IRepository<Employee> _empRepo;
        private readonly IMapper _mapper;
        public EmployeeCollectionController(IRepository<Employee> empRepo, IMapper mapper)
        {
            _empRepo = empRepo;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("addEmployees")]
        public async Task AddEmployeeCollection([FromBody]IEnumerable<EmployeeDto> emps)
        {
            var empEntity = _mapper.Map<List<Employee>>(emps);
            foreach (var emp in empEntity)
            {
                _empRepo.Add(emp);
            }
            await _empRepo.SaveChangesAsync();
        }
    }
}
