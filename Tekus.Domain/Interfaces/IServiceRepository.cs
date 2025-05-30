﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

/// <summary>
/// Contrato para la persistencia de Servicios ofrecidos por proveedores.
/// </summary>
public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10);
    Task<IEnumerable<Service>> GetAllByProviderAsync(int providerId);
    Task<Service?> GetByIdAsync(Guid id);
    Task AddAsync(Service service);
    Task UpdateAsync(Service service);
    Task DeleteAsync(Guid id);
}


