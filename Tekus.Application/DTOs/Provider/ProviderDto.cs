namespace Tekus.Application.DTOs.Provider
{
    /// <summary>
    /// DTO que representa un proveedor tal como se devuelve desde la aplicación.
    /// Incluye todos los datos visibles, incluyendo los campos personalizados.
    /// </summary>
    public class ProviderDto
    {
        /// <summary>
        /// Identificador único del proveedor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Número de Identificación Tributaria (NIT) del proveedor.
        /// </summary>
        public string NIT { get; set; }

        /// <summary>
        /// Correo electrónico del proveedor.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Campos personalizados adicionales del proveedor.
        /// Cada clave representa el nombre del campo y el valor su contenido.
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; }
    }
}
