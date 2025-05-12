using AutoMapper;
using Tekus.Application.DTOs.Provider;
using Tekus.Application.DTOs.Services;
using Tekus.Domain.Entities;

namespace Tekus.Application.Mappings
{
    /// <summary>
    /// Perfil de mapeo de AutoMapper que define las conversiones entre entidades del dominio y los DTOs usados en la aplicación.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeos para Provider ↔ DTOs
            // Incluye propiedades como Id, NIT, Name, Email y CustomFields
            CreateMap<Provider, ProviderDto>().ReverseMap();
            CreateMap<Provider, CreateProviderDto>().ReverseMap();
            CreateMap<Provider, UpdateProviderDto>().ReverseMap();

            // Mapeos para Service ↔ DTOs
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();
            CreateMap<Service, UpdateServiceDto>().ReverseMap();
        }
    }
}
