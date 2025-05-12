using AutoMapper;
using Tekus.Application.DTOs.Provider;
using Tekus.Application.DTOs.Service;
using Tekus.Domain.Entities;

namespace Tekus.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configurar los mapeos entre las entidades y los DTOs
            CreateMap<Provider, ProviderDto>().ReverseMap(); // Para mapeo entre Provider y ProviderDto
            CreateMap<Provider, CreateProviderDto>().ReverseMap(); // Para mapeo entre Provider y CreateProviderDto
            CreateMap<Provider, UpdateProviderDto>().ReverseMap(); // Para mapeo entre Provider y UpdateProviderDto

            CreateMap<Service, ServiceDto>().ReverseMap(); // Para mapeo entre Service y ServiceDto
            CreateMap<Service, CreateServiceDto>().ReverseMap(); // Para mapeo entre Service y CreateServiceDto
            CreateMap<Service, UpdateServiceDto>().ReverseMap(); // Para mapeo entre Service y UpdateServiceDto
        }
    }
}
