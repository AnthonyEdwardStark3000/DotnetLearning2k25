using CustomConventionalMiddlewareClass.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Configure the HTTP request pipeline.
// custom middleware extension without implementing IMiddleware interface
app.UseHelloCustomMiddleware();

app.Run();
