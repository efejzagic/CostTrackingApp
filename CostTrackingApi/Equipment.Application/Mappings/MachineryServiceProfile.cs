using AutoMapper;
using Equipment.Application.DTOs.MachineryService;
using Equipment.Application.Features.Machinery.Queries;
using Equipment.Application.Features.MachineryService.Commands;
using Equipment.Application.Features.MachineryService.Queries;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using Equipment.Domain.Entities;

namespace EquipmentService.Profiles
{
    public class MachineryServiceProfile : Profile
    {
        private readonly IMachineryRepository _machineryRepo;
        public MachineryServiceProfile(IMachineryRepository machineryRepo)
        {
            _machineryRepo = machineryRepo;

            CreateMap<GetAllMachineryServiceQuery, MachineryServiceDTO>();

            CreateMap<MachineryService, MachineryServiceDTO>()
                     .ForMember(d => d.Machinery, opt => opt.MapFrom(src => _machineryRepo.GetById(src.MachineryId).Result));

            CreateMap<MachineryServiceCreateDTO, MachineryService>();
            CreateMap<MachineryServiceEditDTO, MachineryService>();
            CreateMap<MachineryService, MachineryServiceMDTO>();


            CreateMap<GetAllMachineryServiceQuery, GetAllMachineryServiceParameter>();
            CreateMap<GetMachineryServiceByIdQuery, MachineryServiceDTO>();

            CreateMap<GetMachineryServiceByNameQuery, MachineryServiceDTO>();
            CreateMap<CreateMachineryServiceCommand, MachineryServiceCreateDTO>();

            CreateMap<CreateMachineryServiceCommand, MachineryService>();
            CreateMap<DeleteMachineryServiceCommand, MachineryService>();
            CreateMap<UpdateMachineryServiceCommand, MachineryService>();

        }

    }

}
