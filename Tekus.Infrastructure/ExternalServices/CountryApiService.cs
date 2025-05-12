using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using Tekus.Domain.Entities;
using Tekus.Domain.ValueObjects;
using System.Linq;

namespace Infrastructure.ExternalServices
{
    /// <summary>
    /// Servicio para interactuar con la API externa que proporciona información sobre países.
    /// </summary>
    public class CountryApiService : ICountryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _countryApiUrl;

        /// <summary>
        /// Constructor para inicializar el servicio de API de países.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación que contiene la URL base de la API.</param>
        /// <param name="httpClient">Cliente HTTP para realizar las solicitudes a la API.</param>
        public CountryApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _countryApiUrl = configuration["CountryApi:BaseUrl"]; // Carga la URL de la API desde appsettings.json
        }

        /// <inheritdoc />
        public async Task<List<Country>> GetCountriesAsync()
        {
            // Realiza la solicitud GET a la API externa
            var response = await _httpClient.GetAsync(_countryApiUrl);

            // Verifica si la respuesta fue exitosa
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var countriesData = JsonConvert.DeserializeObject<List<CountryApiResponse>>(responseData);

                // Mapea los datos a entidades del dominio
                var countries = countriesData.Select(c => new Country(
                    c.Name.Common,
                    new CountryCode(c.Alpha2Code)
                )).ToList();

                return countries;
            }

            throw new Exception("Error al consultar la API de países.");
        }
    }

    /// <summary>
    /// Representa la respuesta de la API externa sobre un país.
    /// </summary>
    public class CountryApiResponse
    {
        /// <summary>
        /// Nombre del país.
        /// </summary>
        public Name Name { get; set; }

        /// <summary>
        /// Código de país ISO 3166-1 alpha-2.
        /// </summary>
        public string Alpha2Code { get; set; }
    }

    /// <summary>
    /// Representa los nombres posibles de un país en diferentes idiomas.
    /// </summary>
    public class Name
    {
        /// <summary>
        /// Nombre común del país (usado como referencia en la entidad).
        /// </summary>
        public string Common { get; set; }
    }
}
