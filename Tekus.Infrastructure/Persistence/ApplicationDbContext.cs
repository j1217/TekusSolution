using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Tekus.Domain.Entities;  // Importar las entidades del dominio

namespace Tekus.Infrastructure.Persistence
{
    /// <summary>
    /// Contexto principal de la base de datos para Tekus.
    /// Maneja la persistencia de las entidades definidas en el dominio.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor de la clase que pasa las opciones del contexto a la clase base.
        /// </summary>
        /// <param name="options">Opciones del contexto de la base de datos.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Definición de las entidades a mapear a la base de datos
        public DbSet<Provider> Providers { get; set; }   // Proveedores
        public DbSet<Service> Services { get; set; }     // Servicios
        public DbSet<Country> Countries { get; set; }   // Países

        // Método para realizar configuraciones específicas en el modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración adicional si es necesaria, como relaciones y restricciones
            modelBuilder.Entity<Provider>().HasKey(p => p.Id);
            modelBuilder.Entity<Service>().HasKey(s => s.Id);
            modelBuilder.Entity<Country>().HasKey(c => c.Id);

            // Configuración de relaciones adicionales, si aplica (por ejemplo, Provider-Services)
            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Services)
                .WithOne(s => s.Provider)
                .HasForeignKey(s => s.ProviderId);
        }
    }
}
