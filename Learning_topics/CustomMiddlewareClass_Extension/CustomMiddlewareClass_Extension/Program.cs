using CustomMiddlewareClass_Extension.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
builder.Services.AddTransient<CustomMiddlewareClass2>();


var app = builder.Build();


app.UseMyCustomMiddleware(); // Use the custom middleware for calling middleware 1
app.UseMyCustomMiddleware2(); // Use the custom middleware for calling middleware 2
app.Run();
    