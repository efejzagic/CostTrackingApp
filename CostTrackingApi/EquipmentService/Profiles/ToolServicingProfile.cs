using AutoMapper;
using EquipmentService.DTO.Machinery;
using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Profiles
{
    public class ToolServicingProfile : Profile
    {
        private readonly IToolRepository _toolRepo;
        public ToolServicingProfile(IToolRepository toolRepo)
        {
            _toolRepo = toolRepo;
            CreateMap<ToolServicing, ToolServicingDTO>()
                     .ForMember(d => d.Tool, opt => opt.MapFrom(src => _toolRepo.GetById(src.ToolId).Result));

            CreateMap<ToolServicingCreateDTO, ToolServicing>();
            CreateMap<ToolServicingEditDTO, ToolServicing>();
            CreateMap<ToolServicing, ToolServicingMDTO>();
            
        }

    }

}
