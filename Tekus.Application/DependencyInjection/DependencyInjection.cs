using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using System.Reflection;
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
        /// Agrega los servicios de aplicación, AutoMapper y FluentValidation al contenedor de dependencias.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <returns>La colección de servicios modificada.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // AutoMapper: Registrar el perfil de mapeo
            services.AddAutoMapper(typeof(MappingProfile));

            // FluentValidation: Registrar todos los validadores del ensamblado actual
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Servicios de aplicación
            services.AddScoped<IProvidersService, ProvidersService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<IReportsService, ReportsService>();

            return services;
        }
    }
}
