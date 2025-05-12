using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

/// <summary>
/// Servicio responsable de generar tokens JWT.
/// </summary>
public class JwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Constructor que recibe la configuración JWT.
    /// </summary>
    /// <param name="jwtSettings">Instancia de configuración JWT inyectada mediante IOptions.</param>
    public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Genera un token JWT para el usuario con los claims proporcionados.
    /// </summary>
    /// <param name="claims">Claims del usuario (identidad, roles, etc.).</param>
    /// <returns>Token JWT como string.</returns>
    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}
