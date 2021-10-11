using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityHeader(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HeaderMiddleware>();
        }

        public static IEndpointConventionBuilder MapPing(this IEndpointRouteBuilder endpoints,string route)
        {
            var pipeline = endpoints.CreateApplicationBuilder()
                            .UseMiddleware<PingPongMiddleware>()
                            .Build();

            return endpoints.Map(route, pipeline)
                            .WithDisplayName("Ping-Pong");
        }
    }
}
