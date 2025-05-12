using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

/// <summary>
/// Contrato para la obtención de países desde un servicio externo.
/// </summary>
public interface ICountryRepository
{
    /// <summary>
    /// Retorna la lista de países disponibles.
    /// </summary>
    Task<IEnumerable<Country>> GetAllAsync();

    /// <summary>
    /// Busca un país por su código (ej: "CO", "US").
    /// </summary>
    Task<Country?> GetByCodeAsync(string countryCode);
}

