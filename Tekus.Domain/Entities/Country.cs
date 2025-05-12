using Tekus.Domain.Common;
using Tekus.Domain.ValueObjects;

namespace Tekus.Domain.Entities
{
    /// <summary>
    /// Representa un país dentro del sistema.
    /// Aunque los países se consultan desde una API externa, la entidad es representada internamente.
    /// </summary>
    public class Country : BaseEntity
    {
        /// <summary>
        /// Nombre del país.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Código del país (usando un ValueObject para el código).
        /// </summary>
        public CountryCode Code { get; private set; }

        /// <summary>
        /// Constructor para crear un nuevo país.
        /// Se asegura de que el nombre sea válido.
        /// </summary>
        /// <param name="name">Nombre del país.</param>
        /// <param name="code">Código del país.</param>
        public Country(string name, CountryCode code)
        {
            // Validación del nombre del país
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del país es requerido.");

            Name = name;
            Code = code;
        }

        // Constructor privado para frameworks de persistencia como Entity Framework Core
        private Country() { }
    }
}
