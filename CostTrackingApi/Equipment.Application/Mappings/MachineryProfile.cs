using AutoMapper;
using Equipment.Application.DTOs.Machinery;
using Equipment.Application.Features.Machinery.Queries;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using Equipment.Domain.Entities;

namespace Equipment.Application.Mappings
{
    public class MachineryProfile : Profile
    {
        private readonly IMachineryServicingRepository _machineryServicingRepository;
        public MachineryProfile(IMachineryServicingRepository machineryServicingRepository)
        {
            _machineryServicingRepository = machineryServicingRepository;

            CreateMap<GetAllMachineryQuery, MachineryDTO>();


            CreateMap<Machinery, MachineryDTO>()
                .PreserveReferences()
                .ForMember(d => d.ServicingHistory, opt => opt.MapFrom(src => _machineryServicingRepository.GetServicingByMachineryId(src.Id).Result))
                .ForMember(d => d.MaintenanceHistory, opt => opt.MapFrom(src => _machineryServicingRepository.GetMaintenanceByMachineryId(src.Id).Result));

            CreateMap<MachineryCreateDTO, Machinery>()
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));
            ;
            CreateMap<MachineryEditDTO, Machinery>()
                   .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));


            CreateMap<Machinery, MachineryMSDTO>();
            CreateMap<GetAllMachineryQuery, GetAllMachineryParameter>();
            CreateMap<GetMachineryByIdQuery, MachineryDTO>();

            CreateMap<GetMachineryByNameQuery, MachineryDTO>();

        }

    }

}
