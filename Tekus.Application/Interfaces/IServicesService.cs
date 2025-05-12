using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Services;

namespace Tekus.Application.Interfaces
{
    /// <summary>
    /// Define los métodos para la gestión de servicios ofrecidos por los proveedores.
    /// </summary>
    public interface IServicesService
    {
        /// <summary>
        /// Obtiene una lista paginada de servicios con una búsqueda opcional por descripción o nombre.
        /// </summary>
        /// <param name="search">Texto de búsqueda para filtrar servicios.</param>
        /// <param name="page">Número de página para la paginación (por defecto 1).</param>
        /// <param name="pageSize">Cantidad de elementos por página (por defecto 10).</param>
        /// <returns>Lista de servicios en formato DTO.</returns>
        Task<List<ServiceDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Obtiene un servicio específico por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del servicio.</param>
        /// <returns>Servicio en formato DTO si existe, null si no se encuentra.</returns>
        Task<ServiceDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea un nuevo servicio con los datos proporcionados.
        /// </summary>
        /// <param name="dto">DTO con los datos para crear el servicio.</param>
        /// <returns>Servicio creado en formato DTO.</returns>
        Task<ServiceDto> CreateAsync(CreateServiceDto dto);

        /// <summary>
        /// Actualiza un servicio existente identificado por su ID con los nuevos datos proporcionados.
        /// </summary>
        /// <param name="id">ID del servicio a actualizar.</param>
        /// <param name="dto">DTO con los datos a actualizar.</param>
        /// <returns>Servicio actualizado en formato DTO o null si no se encontró.</returns>
        Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceDto dto);

        /// <summary>
        /// Elimina un servicio por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del servicio a eliminar.</param>
        /// <returns>True si se eliminó correctamente, false si no se encontró.</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
