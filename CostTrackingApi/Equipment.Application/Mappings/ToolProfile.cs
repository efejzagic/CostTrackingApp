using AutoMapper;
using Equipment.Application.Interfaces;
using Equipment.Application.DTOs.Tool;
using Equipment.Domain.Entities;
using Equipment.Application.Features.Tool.Commands;
using Equipment.Application.Features.Tool.Queries;
using Equipment.Application.Parameters.Tool;


namespace EquipmentService.Profiles
{
    public class ToolProfile : Profile
    {
        private readonly IToolServicingRepository _toolServicingRepo;
        public ToolProfile(IToolServicingRepository toolServicingRepo)
        {
            _toolServicingRepo = toolServicingRepo;

            CreateMap<GetAllToolQuery, ToolDTO>();

            CreateMap<Tool, ToolDTO>()
                .PreserveReferences()
                .ForMember(d => d.ServicingHistory, opt => opt.MapFrom(src => _toolServicingRepo.GetServicingByToolId(src.Id).Result));
            CreateMap<ToolCreateDTO, Tool>();
            CreateMap<ToolEditDTO, Tool>();
            CreateMap<Tool, ToolTSDTO>();

            CreateMap<GetAllToolQuery, GetAllToolParameter>();
            CreateMap<GetToolByIdQuery, ToolDTO>();

            CreateMap<GetToolByNameQuery, ToolDTO>();
            CreateMap<CreateToolCommand, ToolCreateDTO>();

            CreateMap<CreateToolCommand, Tool>();
            CreateMap<DeleteToolCommand, Tool>();
            CreateMap<UpdateToolCommand, Tool>();


        }
    }
}
