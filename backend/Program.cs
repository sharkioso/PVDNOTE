using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DBContext>(options => 
    options.UseNpgsql("Host=localhost;Port=5432;Database=PVD_Note;Username=postgres;Password=1111;"));

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowFrontend", policy => 
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});

// Добавляем контроллеры
builder.Services.AddControllers();

var app = builder.Build();

// Автоматические миграции при запуске
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DBContext>();
    db.Database.Migrate();
}



app.UseCors("AllowFrontend");
app.MapControllers(); // Для работы AuthController и других

app.MapGet("/", () => "Hello World!");

app.Run();