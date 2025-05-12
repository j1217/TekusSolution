using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Country;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.ExternalContracts; // ← Interfaz usada en lugar de la clase concreta

namespace Tekus.Application.Services
{
    /// <summary>
    /// Servicio para manejar la lógica de negocio relacionada con los países.
    /// </summary>
    public class CountriesService : ICountriesService
    {
        private readonly ICountryApiService _countryApiService;

        /// <summary>
        /// Constructor para inicializar el servicio de países.
        /// </summary>
        /// <param name="countryApiService">Servicio que interactúa con la API externa de países.</param>
        public CountriesService(ICountryApiService countryApiService)
        {
            _countryApiService = countryApiService;
        }

        /// <summary>
        /// Obtiene todos los países desde la API externa.
        /// </summary>
        /// <returns>Lista de países como DTOs.</returns>
        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            var countries = await _countryApiService.GetCountriesAsync(); // Llamada al servicio externo

            var countryDtos = new List<CountryDto>();
            foreach (var country in countries)
            {
                countryDtos.Add(new CountryDto(country.Name, country.Code.Value));
            }

            return countryDtos;
        }
    }
}
