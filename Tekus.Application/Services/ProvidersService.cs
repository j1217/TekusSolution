using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekus.Application.DTOs.Services;
using Tekus.Application.Interfaces;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Services
{
    /// <summary>
    /// Implementación de los servicios relacionados con los proveedores y sus servicios.
    /// </summary>
    public class ProvidersService : IProvidersService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor para inicializar el servicio de proveedores.
        /// </summary>
        /// <param name="providerRepository">Repositorio de proveedores</param>
        /// <param name="mapper">Instancia de AutoMapper para mapear entre DTOs y entidades</param>
        public ProvidersService(IProviderRepository providerRepository, IMapper mapper)
        {
            _providerRepository = providerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los servicios asociados a un proveedor específico.
        /// </summary>
        /// <param name="search">Búsqueda opcional de servicios por nombre</param>
        /// <param name="page">Número de página para paginación</param>
        /// <param name="pageSize">Tamaño de página para paginación</param>
        /// <returns>Lista de servicios mapeada a DTOs</returns>
        public async Task<List<ServiceDto>> GetAllAsync(string search = null, int page = 1, int pageSize = 10)
        {
            // Obtención de servicios de la base de datos (puedes filtrar por nombre si 'search' no es nulo)
            var services = await _providerRepository.GetAllServicesAsync(search, page, pageSize);

            // Mapeo de entidades a DTOs
            var serviceDtos = _mapper.Map<List<ServiceDto>>(services);
            return serviceDtos;
        }

        /// <summary>
        /// Obtiene un servicio por su ID.
        /// </summary>
        /// <param name="id">ID del servicio</param>
        /// <returns>DTO del servicio</returns>
        public async Task<ServiceDto> GetByIdAsync(int id)
        {
            // Obtención del servicio de la base de datos
            var service = await _providerRepository.GetServiceByIdAsync(id);

            // Mapeo de la entidad a DTO
            var serviceDto = _mapper.Map<ServiceDto>(service);
            return serviceDto;
        }

        /// <summary>
        /// Crea un nuevo servicio.
        /// </summary>
        /// <param name="dto">DTO con la información del nuevo servicio</param>
        /// <returns>DTO del servicio creado</returns>
        public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
        {
            // Mapeo del DTO a la entidad
            var service = _mapper.Map<Service>(dto);

            // Creación del servicio en la base de datos
            await _providerRepository.AddServiceAsync(service);

            // Mapeo de la entidad creada a DTO
            var serviceDto = _mapper.Map<ServiceDto>(service);
            return serviceDto;
        }

        /// <summary>
        /// Actualiza un servicio existente.
        /// </summary>
        /// <param name="id">ID del servicio a actualizar</param>
        /// <param name="dto">DTO con los nuevos datos del servicio</param>
        /// <returns>DTO del servicio actualizado</returns>
        public async Task<ServiceDto> UpdateAsync(int id, UpdateServiceDto dto)
        {
            // Obtención del servicio existente
            var service = await _providerRepository.GetServiceByIdAsync(id);

            if (service == null)
            {
                // Si el servicio no existe, lanzamos una excepción
                throw new KeyNotFoundException($"El servicio con ID {id} no fue encontrado.");
            }

            // Mapeo de los datos de actualización
            _mapper.Map(dto, service);

            // Actualización del servicio en la base de datos
            await _providerRepository.UpdateServiceAsync(service);

            // Mapeo de la entidad actualizada a DTO
            var serviceDto = _mapper.Map<ServiceDto>(service);
            return serviceDto;
        }

        /// <summary>
        /// Elimina un servicio por su ID.
        /// </summary>
        /// <param name="id">ID del servicio a eliminar</param>
        /// <returns>Indicador de éxito de la eliminación</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            // Obtención del servicio a eliminar
            var service = await _providerRepository.GetServiceByIdAsync(id);

            if (service == null)
            {
                // Si el servicio no existe, lanzamos una excepción
                throw new KeyNotFoundException($"El servicio con ID {id} no fue encontrado.");
            }

            // Eliminación del servicio
            await _providerRepository.DeleteServiceAsync(service);
            return true;
        }
    }
}
