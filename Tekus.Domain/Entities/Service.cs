using Tekus.Domain.Common;
using Tekus.Domain.ValueObjects;

namespace Tekus.Domain.Entities
{
    /// <summary>
    /// Representa un servicio ofrecido por un proveedor en un país determinado.
    /// Cada servicio tiene un precio y una duración.
    /// </summary>
    public class Service : BaseEntity
    {
        /// <summary>
        /// Nombre del servicio.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Id del proveedor asociado a este servicio.
        /// </summary>
        public int ProviderId { get; private set; }

        /// <summary>
        /// Entidad del proveedor asociado.
        /// </summary>
        public Provider Provider { get; private set; }

        /// <summary>
        /// Id del país asociado a este servicio.
        /// </summary>
        public int CountryId { get; private set; }

        /// <summary>
        /// Entidad del país asociado.
        /// </summary>
        public Country Country { get; private set; }

        /// <summary>
        /// Precio del servicio. Representado como un ValueObject para asegurar su validez.
        /// </summary>
        public Price Price { get; private set; }

        /// <summary>
        /// Duración del servicio. Representado como un ValueObject para asegurar su validez.
        /// </summary>
        public ServiceDuration Duration { get; private set; }

        /// <summary>
        /// Constructor para crear un nuevo servicio.
        /// Se asegura de que todos los parámetros sean válidos.
        /// </summary>
        /// <param name="name">Nombre del servicio.</param>
        /// <param name="providerId">Id del proveedor asociado.</param>
        /// <param name="countryId">Id del país asociado.</param>
        /// <param name="price">Precio del servicio.</param>
        /// <param name="duration">Duración del servicio.</param>
        public Service(string name, int providerId, int countryId, Price price, ServiceDuration duration)
        {
            // Validación del nombre del servicio
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del servicio es requerido.");

            Name = name;
            ProviderId = providerId;
            CountryId = countryId;
            Price = price;
            Duration = duration;
        }

        // Constructor privado para frameworks de persistencia como Entity Framework Core
        private Service() { }
    }
}
