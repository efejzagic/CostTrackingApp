using AutoMapper;
using Equipment.Application.DTOs.ToolService;
using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;
using Equipment.Application.Features.ToolService.Commands;
using Equipment.Application.Features.ToolService.Queries;
using Equipment.Application.Parameters.Machinery;
using Equipment.Application.Parameters.ToolService;

namespace EquipmentService.Profiles
{
    public class ToolServiceProfile : Profile
    {
        private readonly IToolRepository _toolRepo;
        public ToolServiceProfile(IToolRepository toolRepo)
        {
            _toolRepo = toolRepo;
            CreateMap<GetAllToolServiceQuery, ToolServiceDTO>();

            CreateMap<ToolService, ToolServiceDTO>()
                     .ForMember(d => d.Tool, opt => opt.MapFrom(src => _toolRepo.GetById(src.ToolId).Result));

            CreateMap<ToolServiceCreateDTO, ToolService>();
            CreateMap<ToolServiceEditDTO, ToolService>();
            CreateMap<ToolService, ToolServiceMDTO>();


            CreateMap<GetAllToolServiceQuery, GetAllToolServiceParameter>();
            CreateMap<GetToolServiceByIdQuery, ToolServiceDTO>();

            CreateMap<GetToolServiceByNameQuery, ToolServiceDTO>();
            CreateMap<CreateToolServiceCommand, ToolServiceCreateDTO>();

            CreateMap<CreateToolServiceCommand, ToolService>();
            CreateMap<DeleteToolServiceCommand, ToolService>();
            CreateMap<UpdateToolServiceCommand, ToolService>();
        }

    }

}
