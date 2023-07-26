using AutoMapper;
using Equipment.Application.DTOs.MachineryServicing;
using Equipment.Application.Interfaces;
using Equipment.Domain.Entities;

namespace EquipmentService.Profiles
{
    public class MachineryServicingProfile : Profile
    {
        private readonly IMachineryRepository _machineryRepo;
        public MachineryServicingProfile(IMachineryRepository machineryRepo)
        {
            _machineryRepo = machineryRepo;
            CreateMap<MachineryServicing, MachineryServicingDTO>()
                     .ForMember(d => d.Machinery, opt => opt.MapFrom(src => _machineryRepo.GetById(src.MachineryId).Result));

            CreateMap<MachineryServicingCreateDTO, MachineryServicing>();
            CreateMap<MachineryServicingEditDTO, MachineryServicing>();
            CreateMap<MachineryServicing, MachineryServicingMDTO>();
            
        }

    }

}
