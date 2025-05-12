namespace Tekus.Application.DTOs.Provider
{
    /// <summary>
    /// DTO para crear un nuevo proveedor.
    /// Contiene los datos requeridos y opcionales que se deben proporcionar al registrar un proveedor.
    /// </summary>
    public class CreateProviderDto
    {
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
        /// Campos personalizados adicionales que se desean agregar al proveedor.
        /// Cada campo se representa como una clave (nombre) y un valor asociado.
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }
}
