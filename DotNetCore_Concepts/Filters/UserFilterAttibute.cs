using DotNetCore_Concepts.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace DotNetCore_Concepts.Filters
{
    public class UserFilterAttribute : ResultFilterAttribute
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
            //if (typeof(IEnumerable).IsAssignableFrom(result.Value.GetType()))
            //{ 
            //    //Enumerable result
            //}

            var user = new User { Id = 3, FName = "Ria", LName = "Croos" };
            //List<User> userobj =(List<User>) result;
            result.Value = new User { Id = 1, FName = "Newman", LName = "Croos" };
            await next();
            //return base.OnResultExecutionAsync(context, next);
        }
    }
}
