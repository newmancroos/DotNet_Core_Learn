using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;
        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
