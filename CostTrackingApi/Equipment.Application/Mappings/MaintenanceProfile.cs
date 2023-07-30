using AutoMapper;
using Equipment.Application.DTOs.Maintenance;
using Equipment.Domain.Entities;

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
