using Microsoft.EntityFrameworkCore;
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
        public DbSet<Country> Countries { get; set; }    // Países

        /// <summary>
        /// Método para realizar configuraciones específicas en el modelo.
        /// Se definen las relaciones entre las entidades y otras configuraciones personalizadas.
        /// </summary>
        /// <param name="modelBuilder">Objeto que se utiliza para configurar el modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de las claves primarias para las entidades
            modelBuilder.Entity<Provider>().HasKey(p => p.Id);    // Clave primaria para la entidad Provider
            modelBuilder.Entity<Service>().HasKey(s => s.Id);      // Clave primaria para la entidad Service
            modelBuilder.Entity<Country>().HasKey(c => c.Id);      // Clave primaria para la entidad Country

            // Relación de uno a muchos entre Provider y Service
            modelBuilder.Entity<Provider>()
                .HasMany(p => p.Services)   // Un proveedor puede tener muchos servicios
                .WithOne(s => s.Provider)   // Cada servicio tiene un solo proveedor
                .HasForeignKey(s => s.ProviderId);  // La clave foránea de Service apunta a ProviderId

            // Relación de uno a muchos entre Country y Service (si es necesario)
            modelBuilder.Entity<Country>()
                .HasMany(c => c.Services)  // Un país puede tener muchos servicios
                .WithOne(s => s.Country)   // Cada servicio tiene un solo país
                .HasForeignKey(s => s.CountryId);  // La clave foránea de Service apunta a CountryId
        }
    }
}
