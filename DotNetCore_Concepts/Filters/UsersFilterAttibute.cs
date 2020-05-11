using DotNetCore_Concepts.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCore_Concepts.Filters
{
    public class UsersFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if (result?.Value == null || result.StatusCode < 200 || result.StatusCode >= 300)
            {
                await next();
                return;
            }

            //This code identifies the result if Ienumerable or List// We can create a separate filter for List type result
            if (typeof(IEnumerable).IsAssignableFrom(result.Value.GetType()))
            {
                //Enumerable result
            }

            result.Value = new List<User> {
                new User{ Id=1, FName="Newman", LName="Croos"},
                new User{ Id=2, FName="Nithin.V", LName="Croos"}
            };

            await next();
            //return base.OnResultExecutionAsync(context, next);
        }
    }
}
