using AutoMapper;
using Equipment.Application.Interfaces;
using Equipment.Application.DTOs.Tool;
using Equipment.Domain.Entities;
using Equipment.Application.Features.Tool.Commands;
using Equipment.Application.Features.Tool.Queries;
using Equipment.Application.Parameters.Tool;


namespace Equipment.Application.Mappings
{
    public class ToolProfile : Profile
    {
        public ToolProfile()
        {

            CreateMap<GetAllToolQuery, ToolDTO>();

            CreateMap<Tool, ToolDTO>();
            CreateMap<ToolCreateDTO, Tool>();
            CreateMap<ToolEditDTO, Tool>();

            CreateMap<GetAllToolQuery, GetAllToolParameter>();
            CreateMap<GetToolByIdQuery, ToolDTO>();

            CreateMap<GetToolByNameQuery, ToolDTO>();
            CreateMap<CreateToolCommand, ToolCreateDTO>();

            CreateMap<CreateToolCommand, Tool>();
            CreateMap<DeleteToolCommand, Tool>();
            CreateMap<UpdateToolCommand, Tool>();
            CreateMap<Tool, GetAllToolParameter>();



        }
    }
}
