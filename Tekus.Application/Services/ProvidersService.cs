using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Provider;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Domain.ValueObjects;

namespace Tekus.Application.Services
{
    /// <summary>
    /// Servicio de aplicación encargado de la gestión de proveedores.
    /// </summary>
    public class ProvidersService : IProvidersService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor que inyecta dependencias requeridas.
        /// </summary>
        public ProvidersService(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista paginada de proveedores, opcionalmente filtrada por un término de búsqueda.
        /// </summary>
        public async Task<List<ProviderDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10)
        {
            var providers = await _providerRepository.GetAllAsync(page, pageSize, search);
            return _mapper.Map<List<ProviderDto>>(providers);
        }

        /// <summary>
        /// Obtiene un proveedor específico por su identificador único.
        /// </summary>
        public async Task<ProviderDto> GetByIdAsync(Guid id)
        {
            var provider = await _providerRepository.GetByIdAsync(id);
            if (provider == null)
                throw new ArgumentException("Proveedor no encontrado.");

            return _mapper.Map<ProviderDto>(provider);
        }

        /// <summary>
        /// Crea un nuevo proveedor a partir de los datos proporcionados.
        /// </summary>
        public async Task<ProviderDto> CreateAsync(CreateProviderDto dto)
        {
            var email = new Email(dto.Email);
            var provider = new Provider(dto.Name, email);

            await _providerRepository.AddAsync(provider);
            return _mapper.Map<ProviderDto>(provider);
        }

        /// <summary>
        /// Actualiza los datos de un proveedor existente.
        /// </summary>
        public async Task<ProviderDto> UpdateAsync(Guid id, UpdateProviderDto dto)
        {
            var existing = await _providerRepository.GetByIdAsync(id);
            if (existing == null)
                throw new ArgumentException("Proveedor no encontrado.");

            // Actualización de propiedades permitidas
            existing.UpdateName(dto.Name);
            existing.UpdateEmail(new Email(dto.Email));

            await _providerRepository.UpdateAsync(existing);
            return _mapper.Map<ProviderDto>(existing);
        }

        /// <summary>
        /// Elimina un proveedor por su identificador único.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var exists = await _providerRepository.ExistsAsync(id);
            if (!exists)
                return false;

            await _providerRepository.DeleteAsync(id);
            return true;
        }
    }
}
