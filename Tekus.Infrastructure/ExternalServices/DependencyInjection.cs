using Microsoft.Extensions.DependencyInjection;
using System;
using Tekus.Infrastructure.ExternalServices;
using Tekus.Domain.ExternalContracts;
using Infrastructure.ExternalServices;

namespace Tekus.Infrastructure.ExternalServices
{
    /// <summary>
    /// Clase de extensión para configurar y registrar servicios relacionados con APIs externas.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Agrega los servicios externos necesarios al contenedor de dependencias.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios modificada.</returns>
        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            // Registrar el servicio que consume la API de países
            services.AddHttpClient<ICountryApiService, CountryApiService>(client =>
            {
                client.BaseAddress = new Uri("https://restcountries.com/v3.1/all");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
