using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tekus.Infrastructure.Persistence
{
    /// <summary>
    /// Fábrica para crear instancias del contexto de base de datos en entorno de migraciones.
    /// </summary>
    public class ApplicationDbContextFactory
    {
        /// <summary>
        /// Método que crea una instancia de ApplicationDbContext.
        /// Se utiliza para ejecutar migraciones desde la consola.
        /// </summary>
        public static ApplicationDbContext CreateDbContext(string[] args)
        {
            // Configuración de la cadena de conexión a la base de datos
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("YourConnectionStringHere");  // Aquí se coloca la cadena de conexión.

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
