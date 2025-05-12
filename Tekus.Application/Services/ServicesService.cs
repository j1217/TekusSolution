using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Services;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para manejar la lógica relacionada con los servicios ofrecidos por proveedores.
    /// </summary>
    public class ServicesService : IServicesService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public ServicesService(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<List<ServiceDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10)
        {
            var services = await _serviceRepository.GetAllAsync(search, page, pageSize);
            return _mapper.Map<List<ServiceDto>>(services);
        }

        public async Task<ServiceDto> GetByIdAsync(Guid id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            return service == null ? null : _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            var service = _mapper.Map<Service>(dto);
            await _serviceRepository.AddAsync(service);
            return _mapper.Map<ServiceDto>(service);
        }

        public async Task<ServiceDto> UpdateAsync(Guid id, UpdateServiceDto dto)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id);
            if (existingService == null)
                return null;

            _mapper.Map(dto, existingService);
            await _serviceRepository.UpdateAsync(existingService);

            return _mapper.Map<ServiceDto>(existingService);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingService = await _serviceRepository.GetByIdAsync(id);
            if (existingService == null)
                return false;

            await _serviceRepository.DeleteAsync(existingService.Id);
            return true;
        }
    }
}
