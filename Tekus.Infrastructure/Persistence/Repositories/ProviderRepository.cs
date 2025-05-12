using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la gestión de proveedores.
    /// Permite realizar operaciones CRUD y consultas paginadas/filtradas.
    /// </summary>
    public class ProviderRepository : IProviderRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor que inyecta el contexto de la base de datos.
        /// </summary>
        /// <param name="context">Instancia del contexto de EF Core que representa la base de datos.</param>
        public ProviderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los proveedores, con soporte para paginación, búsqueda por nombre y ordenamiento dinámico.
        /// </summary>
        /// <param name="pageNumber">Número de página (inicia en 1).</param>
        /// <param name="pageSize">Cantidad de elementos por página.</param>
        /// <param name="search">Texto opcional para filtrar por nombre.</param>
        /// <param name="sortBy">Campo por el cual ordenar (ej: "Name").</param>
        /// <param name="ascending">Define si el orden es ascendente o descendente.</param>
        /// <returns>Lista paginada y filtrada de proveedores.</returns>
        public async Task<IEnumerable<Provider>> GetAllAsync(int pageNumber, int pageSize, string? search = null, string? sortBy = null, bool ascending = true)
        {
            IQueryable<Provider> query = _context.Providers.AsNoTracking();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = ascending
                    ? query.OrderBy(p => EF.Property<object>(p, sortBy))
                    : query.OrderByDescending(p => EF.Property<object>(p, sortBy));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un proveedor por su identificador único.
        /// </summary>
        /// <param name="id">Identificador del proveedor.</param>
        /// <returns>Proveedor encontrado o null si no existe.</returns>
        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers.FindAsync(id);
        }

        /// <summary>
        /// Agrega un nuevo proveedor a la base de datos.
        /// </summary>
        /// <param name="provider">Entidad proveedor a agregar.</param>
        public async Task AddAsync(Provider provider)
        {
            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un proveedor existente.
        /// </summary>
        /// <param name="provider">Entidad proveedor con cambios.</param>
        public async Task UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un proveedor por su identificador.
        /// </summary>
        /// <param name="id">Identificador del proveedor a eliminar.</param>
        public async Task DeleteAsync(Guid id)
        {
            var provider = await _context.Providers.FindAsync(id);
            if (provider != null)
            {
                _context.Providers.Remove(provider);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica si un proveedor existe en la base de datos.
        /// </summary>
        /// <param name="id">Identificador a verificar.</param>
        /// <returns>True si existe, false en caso contrario.</returns>
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Providers.AnyAsync(p => p.Id == id);
        }
    }
}
