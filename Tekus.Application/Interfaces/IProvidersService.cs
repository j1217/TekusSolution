using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Provider;

namespace Tekus.Application.Interfaces
{
    /// <summary>
    /// Define los métodos para la gestión de proveedores.
    /// </summary>
    public interface IProvidersService
    {
        Task<List<ProviderDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10);
        Task<ProviderDto> GetByIdAsync(int id);
        Task<ProviderDto> CreateAsync(CreateProviderDto dto);
        Task<ProviderDto> UpdateAsync(int id, UpdateProviderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
