var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
// AddSwaggerGen() registers the services to generate the swagger.json document.
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// 2. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // UseSwagger() exposes the generated swagger.json file.
    app.UseSwagger();
    
    // UseSwaggerUI() serves the interactive Swagger UI web page.
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();