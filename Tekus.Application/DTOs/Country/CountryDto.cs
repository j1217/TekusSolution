using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Application.DTOs.Country
{
    /// <summary>
    /// DTO para representar un país.
    /// </summary>
    public class CountryDto
    {
        /// <summary>
        /// Nombre común del país.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Código de país ISO 3166-1 alpha-2.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Constructor para el DTO de país.
        /// </summary>
        /// <param name="name">Nombre del país.</param>
        /// <param name="countryCode">Código del país.</param>
        public CountryDto(string name, string countryCode)
        {
            Name = name;
            CountryCode = countryCode;
        }
    }
}


