using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tekus.Application.Interfaces;
using Tekus.Application.Services;
using Tekus.Domain.ExternalContracts;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Persistence;
using Tekus.Infrastructure.Persistence.Repositories;
using Tekus.Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Authentication;
using Infrastructure.ExternalServices;

namespace Tekus.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Clase de extensión que centraliza el registro de todos los servicios de infraestructura, autenticación, APIs externas y utilidades.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Agrega todos los servicios necesarios al contenedor de dependencias.
        /// Incluye: autenticación JWT, repositorios, servicios de aplicación, servicios externos y utilitarios.
        /// </summary>
        /// <param name="services">Colección de servicios de la aplicación.</param>
        /// <param name="configuration">Configuración general de la aplicación (appsettings.json, variables de entorno, etc.).</param>
        /// <returns>La colección de servicios modificada con todas las dependencias registradas.</returns>
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddJwtAuthentication(services, configuration);
            AddPersistence(services, configuration);
            AddExternalServices(services);
            AddUtilityServices(services);

            return services;
        }

        /// <summary>
        /// Configura y registra la autenticación JWT en la aplicación.
        /// </summary>
        private static void AddJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind("JwtSettings", jwtSettings);

            if (string.IsNullOrWhiteSpace(jwtSettings.Secret) ||
                string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
                string.IsNullOrWhiteSpace(jwtSettings.Audience))
            {
                throw new InvalidOperationException("Los valores de configuración JWT (Secret, Issuer, Audience) no están definidos correctamente en appsettings.json.");
            }

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        /// <summary>
        /// Registra los servicios de base de datos, repositorios y servicios de aplicación.
        /// </summary>
        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositorios
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();

            // Servicios de aplicación
            services.AddScoped<IProvidersService, ProvidersService>();
            services.AddScoped<IServicesService, ServicesService>();
        }

        /// <summary>
        /// Registra los servicios que consumen APIs externas.
        /// </summary>
        private static void AddExternalServices(IServiceCollection services)
        {
            services.AddHttpClient<ICountryApiService, CountryApiService>(client =>
            {
                client.BaseAddress = new Uri("https://restcountries.com/v3.1/all");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }

        /// <summary>
        /// Registra servicios auxiliares internos, como utilidades de tiempo.
        /// </summary>
        private static void AddUtilityServices(IServiceCollection services)
        {
            services.AddSingleton<DateTimeService>();
        }
    }
}
