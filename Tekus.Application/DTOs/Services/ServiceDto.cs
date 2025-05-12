using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Services
{
    /// <summary>
    /// DTO para representar un servicio.
    /// </summary>
    public class ServiceDto
    {
        /// <summary>
        /// Identificador único del servicio.
        /// </summary>
        public int Id { get; set; }

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
        public List<string> Countries { get; set; } = new(); // Lista de nombres de países
    }
}

