using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

/// <summary>
/// Contrato para la persistencia de Proveedores.
/// </summary>
public interface IProviderRepository
{
    Task<IEnumerable<Provider>> GetAllAsync(int pageNumber, int pageSize, string? search = null, string? sortBy = null, bool ascending = true);
    Task<Provider?> GetByIdAsync(Guid id);
    Task AddAsync(Provider provider);
    Task UpdateAsync(Provider provider);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
