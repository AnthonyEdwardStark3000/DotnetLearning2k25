using MiddlewareSeparateClass.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<CustomMiddlewareClass2>();

var app = builder.Build();

// calling middlewares
app.UseMiddleware<MyCustomMiddleware>();
app.UseMiddleware<CustomMiddlewareClass2>();

app.Run();
