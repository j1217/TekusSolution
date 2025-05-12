using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Services
{
    /// <summary>
    /// DTO para crear un nuevo servicio.
    /// </summary>
    public class CreateServiceDto
    {
        /// <summary>
        /// Nombre del servicio.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Valor por hora del servicio en USD.
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Lista de países donde el servicio es ofrecido.
        /// </summary>
        public List<int> CountryIds { get; set; } = new(); // Se espera una lista de IDs de países
    }
}
