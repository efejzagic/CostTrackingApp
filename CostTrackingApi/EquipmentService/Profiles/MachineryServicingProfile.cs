using AutoMapper;
using EquipmentService.DTO.Machinery;
using EquipmentService.DTO.MachineryServicing;
using EquipmentService.Interfaces;
using EquipmentService.Models;

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
