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
        // Cargar configuración de JwtSettings desde appsettings.json
        var jwtSettings = new JwtSettings();
        configuration.Bind("JwtSettings", jwtSettings);

        // Validar que los campos requeridos estén definidos
        if (string.IsNullOrWhiteSpace(jwtSettings.Secret) ||
            string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
            string.IsNullOrWhiteSpace(jwtSettings.Audience))
        {
            throw new InvalidOperationException("Los valores de configuración JWT (Secret, Issuer, Audience) no están definidos correctamente en appsettings.json.");
        }

        // Registrar configuración para inyección de dependencias
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        // Convertir la clave secreta en arreglo de bytes
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
                ValidateIssuer = true,                     // Validar el emisor del token
                ValidateAudience = true,                   // Validar el destinatario del token
                ValidateLifetime = true,                   // Validar expiración del token
                ValidateIssuerSigningKey = true,           // Validar firma del token
                ValidIssuer = jwtSettings.Issuer,          // Emisor válido
                ValidAudience = jwtSettings.Audience,      // Audiencia válida
                IssuerSigningKey = new SymmetricSecurityKey(key) // Clave de firma
            };
        });

        return services;
    }
}
