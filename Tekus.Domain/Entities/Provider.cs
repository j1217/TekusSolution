using Tekus.Domain.Common;
using Tekus.Domain.ValueObjects;

namespace Tekus.Domain.Entities
{
    /// <summary>
    /// Representa un proveedor dentro del sistema. 
    /// El proveedor tiene un NIT, nombre y correo electrónico, todos validados.
    /// </summary>
    public class Provider : BaseEntity
    {
        /// <summary>
        /// Número de Identificación Tributaria (NIT) del proveedor.
        /// Este campo es obligatorio y debe ser único por proveedor.
        /// </summary>
        public string NIT { get; private set; }

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
        /// Se asegura de que el NIT, nombre y correo electrónico sean válidos.
        /// </summary>
        /// <param name="nit">Número de identificación tributaria del proveedor.</param>
        /// <param name="name">Nombre del proveedor.</param>
        /// <param name="email">Correo electrónico del proveedor.</param>
        public Provider(string nit, string name, Email email)
        {
            if (string.IsNullOrWhiteSpace(nit))
                throw new ArgumentException("El NIT del proveedor es requerido.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del proveedor es requerido.");

            NIT = nit;
            Name = name;
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        /// <summary>
        /// Actualiza el NIT del proveedor.
        /// </summary>
        /// <param name="nit">Nuevo NIT.</param>
        public void UpdateNIT(string nit)
        {
            if (string.IsNullOrWhiteSpace(nit))
                throw new ArgumentException("El NIT del proveedor es requerido.");

            NIT = nit;
        }

        /// <summary>
        /// Actualiza el nombre del proveedor.
        /// </summary>
        /// <param name="name">Nuevo nombre.</param>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del proveedor es requerido.");

            Name = name;
        }

        /// <summary>
        /// Actualiza el correo electrónico del proveedor.
        /// </summary>
        /// <param name="email">Nuevo correo electrónico.</param>
        public void UpdateEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        /// <summary>
        /// Constructor privado requerido por frameworks de persistencia como Entity Framework Core.
        /// </summary>
        private Provider() { }
    }
}
