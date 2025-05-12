using Tekus.Domain.Common;

namespace Tekus.Domain.Entities
{
    /// <summary>
    /// Representa un campo personalizado asociado a un proveedor.
    /// Cada campo se almacena como un par clave-valor y pertenece a un proveedor específico.
    /// </summary>
    public class ProviderCustomField : BaseEntity
    {
        /// <summary>
        /// Clave o nombre del campo personalizado (ejemplo: "Teléfono Marte").
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Valor asociado al campo personalizado.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Identificador del proveedor al que pertenece este campo.
        /// </summary>
        public Guid ProviderId { get; private set; }

        /// <summary>
        /// Referencia de navegación al proveedor.
        /// </summary>
        public Provider Provider { get; private set; }

        /// <summary>
        /// Constructor para crear un campo personalizado.
        /// </summary>
        /// <param name="key">Nombre del campo.</param>
        /// <param name="value">Valor del campo.</param>
        /// <param name="providerId">ID del proveedor al que se asocia.</param>
        public ProviderCustomField(string key, string value, Guid providerId)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("La clave del campo personalizado es requerida.");

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("El valor del campo personalizado es requerido.");

            Key = key;
            Value = value;
            ProviderId = providerId;
        }

        /// <summary>
        /// Constructor privado requerido por Entity Framework Core.
        /// </summary>
        private ProviderCustomField() { }
    }
}
