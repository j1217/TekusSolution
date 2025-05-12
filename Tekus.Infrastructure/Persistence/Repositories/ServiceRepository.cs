using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Implementación del repositorio para la entidad Service, utilizando Entity Framework Core.
    /// </summary>
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Service>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10)
        {
            var query = _context.Services.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            return await query
                .OrderBy(s => s.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Service>> GetAllByProviderAsync(int providerId)
        {
            return await _context.Services
                .Where(s => s.ProviderId == providerId)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}
