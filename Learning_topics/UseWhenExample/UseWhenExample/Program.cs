using UseWhenExample.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWhen(
    context => context.Request.Query.ContainsKey("userName"),
    appBranch =>
    {
        appBranch.UseCustomMiddlewareExtension();
    });

// Default/final middleware in pipeline
app.Run(async context =>
{
    await context.Response.WriteAsync("This is the final middleware in the pipeline.\n");
});

app.Run();
