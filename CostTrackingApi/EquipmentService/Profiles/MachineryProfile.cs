using AutoMapper;
using EquipmentService.DTO.Machinery;
using EquipmentService.Models;

namespace EquipmentService.Profiles
{
    public class MachineryProfile : Profile
    {
        public MachineryProfile()
        {
            CreateMap<Machinery, MachineryDTO>();
            CreateMap<MachineryCreateDTO, Machinery>()
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));
;
            CreateMap<MachineryEditDTO, Machinery>()
                   .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));
        }

    }

}
