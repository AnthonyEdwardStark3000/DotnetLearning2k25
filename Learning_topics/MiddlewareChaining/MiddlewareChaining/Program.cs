var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.Use(async (HttpContext context,RequestDelegate next) =>
{
    await context.Response.WriteAsync("\nMiddleware 1");
    await next(context);
    await context.Response.WriteAsync("\nMiddleware 1 continuation");
});

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("\nMiddleware 2");
    await next(context);
    await context.Response.WriteAsync("\nMiddleware 2 continuation");
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("\nMiddleware 3");
});
app.Run();
