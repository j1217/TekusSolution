using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Domain.ExternalContracts
{
    /// <summary>
    /// Contrato para servicios que consultan países desde fuentes externas.
    /// </summary>
    public interface ICountryApiService
    {
        /// <summary>
        /// Obtiene una lista de países desde una fuente externa.
        /// </summary>
        /// <returns>Lista de entidades de país.</returns>
        Task<List<Country>> GetCountriesAsync();
    }
}
