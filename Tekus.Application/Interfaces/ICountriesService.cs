using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Country;

namespace Tekus.Application.Interfaces
{
    /// <summary>
    /// Interfaz para los servicios relacionados con los países.
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Obtiene todos los países desde la API externa.
        /// </summary>
        /// <returns>Lista de países.</returns>
        Task<List<CountryDto>> GetAllCountriesAsync();
    }
}
