using AutoMapper;
using EquipmentService.DTO.Machinery;
using EquipmentService.DTO.Tool;
using EquipmentService.Interfaces;
using EquipmentService.Models;

namespace EquipmentService.Profiles
{
    public class ToolProfile : Profile
    {
        private readonly IToolServicingRepository _toolServicingRepo;
        public ToolProfile(IToolServicingRepository toolServicingRepo)
        {
            _toolServicingRepo = toolServicingRepo;
            CreateMap<Tool, ToolDTO>()
                .PreserveReferences()
                .ForMember(d => d.ServicingHistory, opt => opt.MapFrom(src => _toolServicingRepo.GetServicingByToolId(src.Id).Result));
            CreateMap<ToolCreateDTO, Tool>();
            CreateMap<ToolEditDTO, Tool>();
            CreateMap<Tool, ToolTSDTO>();

        }
    }
}
