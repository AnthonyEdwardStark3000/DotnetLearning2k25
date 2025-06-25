var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{fileName}.{extension}", async context =>
    {
        var fileName = context.Request.RouteValues["fileName"];
        var extension = context.Request.RouteValues["extension"];

        await context.Response.WriteAsync($"File Name: {fileName}, Extension: {extension}");
    });
    
    endpoints.Map("users/{name:length(3,10):alpha}", async context =>
    {
        string userName = context.Request.RouteValues["name"]?.ToString();  
        await context.Response.WriteAsync($"User name that satisfies the condition is provided {userName}");
    });

    //sales-report/2030/apr
    endpoints.Map(" {year:int:min(1900)}/{month:regex(^(apr|may|june)$)}",async context =>
    {
        int yearEntered = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? monthEntered = context.Request.RouteValues["month"]?.ToString();
        await context.Response.WriteAsync($"month:{monthEntered} {yearEntered}");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync("No matching route found.");
});

app.Run();
