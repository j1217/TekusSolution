using Tekus.Domain.Common;
using Tekus.Domain.ValueObjects;

namespace Tekus.Domain.Entities
{
    /// <summary>
    /// Representa un proveedor dentro del sistema. 
    /// El proveedor tiene un nombre y un email, ambos deben ser validados.
    /// </summary>
    public class Provider : BaseEntity
    {
        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Correo electrónico del proveedor, validado a través de un ValueObject (Email).
        /// </summary>
        public Email Email { get; private set; }

        /// <summary>
        /// Lista de servicios ofrecidos por el proveedor.
        /// </summary>
        public ICollection<Service> Services { get; private set; } = new List<Service>();

        /// <summary>
        /// Constructor para crear un nuevo proveedor.
        /// Se asegura de que el nombre y el correo electrónico sean válidos.
        /// </summary>
        /// <param name="name">Nombre del proveedor.</param>
        /// <param name="email">Correo electrónico del proveedor.</param>
        public Provider(string name, Email email)
        {
            // Validación de nombre del proveedor
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del proveedor es requerido.");

            Name = name;
            Email = email;
        }

        // Constructor privado para frameworks de persistencia como Entity Framework Core
        private Provider() { }
    }
}
