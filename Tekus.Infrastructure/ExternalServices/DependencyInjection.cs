using Infrastructure.ExternalServices;
using Microsoft.Extensions.DependencyInjection;
using Tekus.Infrastructure.ExternalServices;

namespace Tekus.Infrastructure.ExternalServices
{
    /// <summary>
    /// Clase de extensión para configurar y registrar servicios relacionados con APIs externas.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Agrega el servicio que consume la API externa de países al contenedor de dependencias.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios modificada.</returns>
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            // Registrar el servicio que consume la API de países
            services.AddHttpClient<ICountryApiService, CountryApiService>(client =>
            {
                // Configuración de la URL base de la API
                client.BaseAddress = new Uri("https://restcountries.com/v3.1/all");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
