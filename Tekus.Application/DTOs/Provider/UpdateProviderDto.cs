namespace Tekus.Application.DTOs.Provider
{
    /// <summary>
    /// DTO para actualizar los datos de un proveedor existente.
    /// Se utiliza para modificar la información básica del proveedor
    /// y sus campos personalizados representados como clave-valor.
    /// </summary>
    public class UpdateProviderDto
    {
        /// <summary>
        /// Nombre actualizado del proveedor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Número de Identificación Tributaria (NIT) actualizado del proveedor.
        /// </summary>
        public string NIT { get; set; }

        /// <summary>
        /// Correo electrónico actualizado del proveedor.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Campos personalizados actualizados para el proveedor.
        /// Cada entrada representa un campo dinámico clave-valor que será persistido como entidad.
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }
}
