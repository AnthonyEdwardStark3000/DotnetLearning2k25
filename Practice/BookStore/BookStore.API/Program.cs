using BookStore.Application.Interfaces;
using BookStore.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔧 Register Services
// builder.Services.AddScoped<IBookService, InMemoryService>();
builder.Services.AddSingleton<IBookService, InMemoryService>();

// 🔧 Add Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Enable Swagger in All Environments (optional)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API V1");
    c.RoutePrefix = "swagger"; // optional, defaults to swagger
});

// 📌 Middleware
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
