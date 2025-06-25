
namespace MiddlewareSeparateClass.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello from MyCustomMiddleware!\n");
            // Call the next middleware in the pipeline
            await next(context);
        }
    }
}
