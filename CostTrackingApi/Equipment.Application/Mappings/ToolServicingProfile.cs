using AutoMapper;
using Equipment.Application.DTOs.MachineryServicing;
using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;
using Equipment.Application.DTOs.ToolServicing;

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
