using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace UseWhenExample.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddlewareExtension
    {
        private readonly RequestDelegate _next;

        public CustomMiddlewareExtension(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string name = httpContext.Request.Query["userName"];
            await httpContext.Response.WriteAsync($"Hello {name}\n");
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensionExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewareExtension(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddlewareExtension>();
        }
    }
}
