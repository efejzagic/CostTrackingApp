using AutoMapper;
using EquipmentService.DTO.Tool;
using EquipmentService.Models;

namespace EquipmentService.Profiles
{
    public class ToolProfile : Profile
    {

        public ToolProfile()
        {
            CreateMap<Tool, ToolDTO>();
            CreateMap<ToolCreateDTO, Tool>();
            CreateMap<ToolEditDTO, Tool>();
        }
    }
}
