using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tekus.Infrastructure.DependencyInjection;
using Tekus.Infrastructure.Persistence; // Para SeedData
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agrega todos los servicios necesarios (repositorios, app services, JWT, etc.)
builder.Services.AddProjectServices(builder.Configuration);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ejecutar SeedData al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

// Middleware de desarrollo (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Seguridad: Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Rutas de controladores
app.MapControllers();

// Iniciar la aplicación
app.Run();
