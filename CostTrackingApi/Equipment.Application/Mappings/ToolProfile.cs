using AutoMapper;
using Equipment.Application.Interfaces;
using Equipment.Application.DTOs.Tool;
using Equipment.Domain.Entities;

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
