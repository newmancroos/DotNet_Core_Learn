using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore_Concepts.Entities;
using DotNetCore_Concepts.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Concepts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get")]
        [UserFilter]
        public async Task<ActionResult>  GetUser()
        {
            await Task.Delay(200);
            return Ok(new List<User> {
                new User{ Id=1, FName="Newman", LName="Croos"},
                new User{ Id=2, FName="Nithin", LName="Croos"}
            });
        }

        [HttpGet("getAll")]
        [UsersFilter]
        public async Task<ActionResult> GetUsers()
        {
            await Task.Delay(200);
            return Ok(new List<User> {
                new User{ Id=1, FName="Newman", LName="Croos"},
                new User{ Id=2, FName="Nithin", LName="Croos"}
            });
        }
    }
}