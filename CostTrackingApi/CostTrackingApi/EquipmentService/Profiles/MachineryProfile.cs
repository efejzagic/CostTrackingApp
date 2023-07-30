using AutoMapper;
using EquipmentService.DTO.Machinery;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Profiles
{
    public class MachineryProfile : Profile
    {
        private readonly IMachineryServicingRepository _machineryServicingRepository;
        public MachineryProfile(IMachineryServicingRepository machineryServicingRepository)
        {
            _machineryServicingRepository = machineryServicingRepository;

            CreateMap<Machinery, MachineryDTO>()
                .PreserveReferences()
                .ForMember(d => d.ServicingHistory, opt => opt.MapFrom(src => _machineryServicingRepository.GetServicingByMachineryId(src.Id).Result));
            CreateMap<MachineryCreateDTO, Machinery>()
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));
            ;
            CreateMap<MachineryEditDTO, Machinery>()
                   .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));

            CreateMap<Machinery, MachineryMSDTO>();
        }

    }

}
