using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Provider
{
    /// <summary>
    /// DTO para crear un nuevo proveedor.
    /// </summary>
    public class CreateProviderDto
    {
        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// NIT del proveedor.
        /// </summary>
        public string NIT { get; set; }

        /// <summary>
        /// Correo electrónico del proveedor.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Campos personalizados adicionales que se desean agregar al proveedor.
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; } = new();
    }
}

