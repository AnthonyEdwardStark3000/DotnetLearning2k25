var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options=>{
    options.ConstraintMap.Add("months",typeof(MonthsCustomConstraints));
});
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints=>{
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}",async context=>{
        await context.Response.WriteAsync($"year:{month}");
    });
});
app.Run();
