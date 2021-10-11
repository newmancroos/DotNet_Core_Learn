using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares
{
    public class PingPongMiddleware
    {
        public PingPongMiddleware(RequestDelegate next)
        { 
            
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Pong");
        }
    }
}
