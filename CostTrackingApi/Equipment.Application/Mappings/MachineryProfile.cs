using AutoMapper;
using Equipment.Application.DTOs.Machinery;
using Equipment.Application.Features.Machinery.Commands;
using Equipment.Application.Features.Machinery.Queries;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using Equipment.Domain.Entities;

namespace Equipment.Application.Mappings
{
    public class MachineryProfile : Profile
    {
       
        public MachineryProfile()
        {


            CreateMap<GetAllMachineryQuery, MachineryDTO>();


            CreateMap<Machinery, MachineryDTO>();
            

            CreateMap<MachineryCreateDTO, Machinery>()
                .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));
            ;
            CreateMap<MachineryEditDTO, Machinery>()
                   .ForMember(dest => dest.ProductionYear, opt => opt.MapFrom(src => new DateOnly(src.ProductionYear.Year, src.ProductionYear.Month, src.ProductionYear.Day)));


     
            CreateMap<GetAllMachineryQuery, GetAllMachineryParameter>();
            CreateMap<GetMachineryByIdQuery, MachineryDTO>();

            CreateMap<GetMachineryByNameQuery, MachineryDTO>();
            CreateMap<CreateMachineryCommand, MachineryCreateDTO>();

            CreateMap<CreateMachineryCommand, Machinery>();
            CreateMap<DeleteMachineryCommand, Machinery>();
            CreateMap<UpdateMachineryCommand, Machinery>();

        }

    }

}
