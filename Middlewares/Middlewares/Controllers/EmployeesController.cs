using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middlewares.Controllers
{
    [ApiController]
    [Route("Employee")]
    public class EmployeesController : ControllerBase
    {
        [HttpGet("get")]
        public IActionResult GetEmployees()
        {
            return Ok("Success");
        }
    }
}
