using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using Tekus.Domain.Entities;
using Tekus.Domain.ValueObjects;
using System.Linq;
using Tekus.Domain.ExternalContracts; 

namespace Infrastructure.ExternalServices
{
    /// <summary>
    /// Servicio para interactuar con la API externa que proporciona información sobre países.
    /// </summary>
    public class CountryApiService : ICountryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _countryApiUrl;

        public CountryApiService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _countryApiUrl = configuration["CountryApi:BaseUrl"];
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            var response = await _httpClient.GetAsync(_countryApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var countriesData = JsonConvert.DeserializeObject<List<CountryApiResponse>>(responseData);

                var countries = countriesData.Select(c => new Country(
                    c.Name.Common,
                    new CountryCode(c.Alpha2Code)
                )).ToList();

                return countries;
            }

            throw new Exception("Error al consultar la API de países.");
        }
    }

    public class CountryApiResponse
    {
        public Name Name { get; set; }
        public string Alpha2Code { get; set; }
    }

    public class Name
    {
        public string Common { get; set; }
    }
}
