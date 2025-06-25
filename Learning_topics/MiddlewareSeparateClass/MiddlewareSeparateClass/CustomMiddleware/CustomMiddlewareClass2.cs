namespace MiddlewareSeparateClass.CustomMiddleware
{
    public class CustomMiddlewareClass2:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context,RequestDelegate next) { 
            await context.Response.WriteAsync("Hello from CustomMiddlewareClass2!\n");
            await next(context);
        }
    }
}
