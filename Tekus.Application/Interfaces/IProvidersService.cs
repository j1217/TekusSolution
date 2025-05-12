using System;
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
        /// <summary>
        /// Obtiene una lista paginada de proveedores con una búsqueda opcional por nombre.
        /// </summary>
        /// <param name="search">Texto de búsqueda para filtrar proveedores por nombre.</param>
        /// <param name="page">Número de página para la paginación (por defecto 1).</param>
        /// <param name="pageSize">Tamaño de página, es decir, cantidad de elementos por página (por defecto 10).</param>
        /// <returns>Una lista de proveedores que coincidan con los criterios de búsqueda y paginación.</returns>
        Task<List<ProviderDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Obtiene un proveedor específico por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del proveedor.</param>
        /// <returns>Proveedor encontrado en formato DTO, o null si no existe.</returns>
        Task<ProviderDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Crea un nuevo proveedor a partir de los datos proporcionados.
        /// </summary>
        /// <param name="dto">DTO con los datos necesarios para crear el proveedor.</param>
        /// <returns>Proveedor creado en formato DTO.</returns>
        Task<ProviderDto> CreateAsync(CreateProviderDto dto);

        /// <summary>
        /// Actualiza un proveedor existente con los nuevos datos proporcionados.
        /// </summary>
        /// <param name="id">Identificador único del proveedor a actualizar.</param>
        /// <param name="dto">DTO con los nuevos datos del proveedor.</param>
        /// <returns>Proveedor actualizado en formato DTO.</returns>
        Task<ProviderDto> UpdateAsync(Guid id, UpdateProviderDto dto);

        /// <summary>
        /// Elimina un proveedor según su identificador.
        /// </summary>
        /// <param name="id">Identificador único del proveedor a eliminar.</param>
        /// <returns>True si el proveedor fue eliminado exitosamente; false en caso contrario.</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
