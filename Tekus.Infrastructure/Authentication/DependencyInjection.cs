using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Authentication;

/// <summary>
/// Clase de extensión para registrar el servicio de autenticación JWT.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Agrega la autenticación JWT al contenedor de servicios.
    /// </summary>
    /// <param name="services">Colección de servicios de la aplicación.</param>
    /// <param name="configuration">Configuración de la aplicación (appsettings.json).</param>
    /// <returns>La colección de servicios modificada.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Cargar los valores de configuración desde appsettings.json
        var jwtSettings = new JwtSettings();
        configuration.Bind("JwtSettings", jwtSettings);

        // Registrar configuración para inyección de dependencias
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        // Configurar el esquema de autenticación JWT
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

        return services;
    }
}
