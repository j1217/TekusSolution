using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Tekus.Application.Services;
using Tekus.Application.Interfaces;
using Tekus.Application.Mappings;

namespace Tekus.Application.DependencyInjection
{
    /// <summary>
    /// Clase de extensión para registrar servicios de la capa Application.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Agrega los servicios de aplicación y AutoMapper al contenedor de dependencias.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios modificada.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper: Registrar el perfil de mapeo
            services.AddAutoMapper(typeof(MappingProfile));

            // Servicios de aplicación
            services.AddScoped<IProvidersService, ProvidersService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IServicesService, ServicesService>();

            return services;
        }
    }
}
