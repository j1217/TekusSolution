namespace Tekus.Application.DTOs.Provider
{
    /// <summary>
    /// DTO para actualizar los datos de un proveedor existente.
    /// Se utiliza para modificar la información básica y los campos personalizados de un proveedor.
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
        /// Campos personalizados adicionales que se desean actualizar en el proveedor.
        /// Cada entrada es un par clave-valor que representa un campo dinámico.
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }
}
