using AutoMapper;
using EquipmentService.DTO.Maintenance;
using EquipmentService.Models;

namespace EquipmentService.Profiles
{
    public class MaintenanceProfile : Profile
    {
        public MaintenanceProfile()
        {
            CreateMap<Maintenance, MaintenanceDTO> ();
            
        }
    }
}
