using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Infrastructure
{
    /// <summary>
    /// Clase estática para registrar la inyección de dependencias para la persistencia.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registra los servicios de persistencia y el DbContext en el contenedor de dependencias.
        /// </summary>
        /// <param name="services">Colección de servicios a la que se le agregan los servicios de persistencia.</param>
        /// <param name="configuration">Configuración de la aplicación (appsettings.json).</param>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración del DbContext usando SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))); // Cadena de conexión desde appsettings.json

            return services;
        }
    }
}
