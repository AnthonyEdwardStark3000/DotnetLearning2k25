var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.Run(async(HttpContext context) =>
{
    context.Response.Headers["Sample-Header"] = "Come on Mr.Stark, show me what you can.";
    context.Response.Headers["Content-type"] = "text/html";
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("<p>Hello World from FirstWebApp!</p>");

    string path = context.Request.Path;
    await context.Response.WriteAsync($"<p>{path}</p>");

    if (context.Request.Method=="GET" && context.Request.Query.ContainsKey("id")) {
        string id = context.Request.Query["id"];
        await context.Response.WriteAsync($"ID received :{id}");


        if (context.Request.Query.ContainsKey("name")) {
            string name = context.Request.Query["name"];
            await context.Response.WriteAsync($"User :{name}");
        }
    }

    string host = context.Request.Headers["host"];
    await context.Response.WriteAsync($"<p>Have a nice day {host}!</p>");

    if (context.Request.Headers.ContainsKey("sample")) {
        string sample = context.Request.Headers["sample"];
        await context.Response.WriteAsync($"<p>Sample data: {sample}</p>");

    }
});

app.Run();
