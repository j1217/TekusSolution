using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Infrastructure.ExternalServices
{
    /// <summary>
    /// Define la interfaz para el servicio que consulta países desde una API externa.
    /// </summary>
    public interface ICountryApiService
    {
        /// <summary>
        /// Obtiene la lista de países desde una fuente externa.
        /// </summary>
        /// <returns>Una lista de objetos <see cref="Country"/>.</returns>
        Task<List<Country>> GetCountriesAsync();
    }
}
