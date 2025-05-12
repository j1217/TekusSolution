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
        /// Incluye los campos personalizados definidos para cada proveedor.
        /// </summary>
        public async Task<IEnumerable<Provider>> GetAllAsync(int pageNumber, int pageSize, string? search = null, string? sortBy = null, bool ascending = true)
        {
            IQueryable<Provider> query = _context.Providers
                .Include(p => p.CustomFields) // Incluir campos personalizados
                .AsNoTracking();

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
        /// Obtiene un proveedor por su identificador único, incluyendo sus campos personalizados.
        /// </summary>
        public async Task<Provider?> GetByIdAsync(Guid id)
        {
            return await _context.Providers
                .Include(p => p.CustomFields) // Incluir campos personalizados
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Agrega un nuevo proveedor a la base de datos.
        /// </summary>
        public async Task AddAsync(Provider provider)
        {
            await _context.Providers.AddAsync(provider);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Actualiza un proveedor existente.
        /// </summary>
        public async Task UpdateAsync(Provider provider)
        {
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Elimina un proveedor por su identificador.
        /// </summary>
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
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Providers.AnyAsync(p => p.Id == id);
        }
    }
}
