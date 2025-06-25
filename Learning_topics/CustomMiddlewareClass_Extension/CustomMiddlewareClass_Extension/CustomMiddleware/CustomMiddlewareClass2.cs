namespace CustomMiddlewareClass_Extension.CustomMiddleware
{
    public class CustomMiddlewareClass2:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context,RequestDelegate next) { 
            await context.Response.WriteAsync("Hello from CustomMiddlewareClass2!\n");
            await next(context);
        }
    }

    public static class MyCustomMiddlewareExtensions2
    {
        public static IApplicationBuilder UseMyCustomMiddleware2(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddlewareClass2>();
        }
    }
}
