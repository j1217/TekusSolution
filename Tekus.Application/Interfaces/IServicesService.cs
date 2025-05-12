using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Services;

namespace Tekus.Application.Interfaces
{
    /// <summary>
    /// Define los métodos para la gestión de servicios.
    /// </summary>
    public interface IServicesService
    {
        Task<List<ServiceDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10);
        Task<ServiceDto> GetByIdAsync(Guid id);
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);
        Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
