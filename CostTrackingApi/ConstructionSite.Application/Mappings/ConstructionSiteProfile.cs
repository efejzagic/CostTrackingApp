using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Features.ConstructionSite.Commands;
using ConstructionSite.Application.Features.ConstructionSite.Queries;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Parameters.ConstructionSite;
using ConstructionSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Mappings
{
    public class ConstructionSiteProfile : Profile
    {
        private readonly IEmployeeRepository _repository;
        public ConstructionSiteProfile(IEmployeeRepository repository)
        {
            _repository = repository;
            CreateMap<Domain.Entities.ConstructionSite, ConstructionSiteDTO>()
             .PreserveReferences()
             .ForMember(d => d.Employees, opt => opt.MapFrom(src => _repository.GetEmployeesByConstructionId(src.Id).Result));

        

            CreateMap<Domain.Entities.ConstructionSite, ConstructionSiteEmployeeDTO>();
            CreateMap<ConstructionSiteCreateDTO, Domain.Entities.ConstructionSite>();
            CreateMap<ConstructionSiteEditDTO, Domain.Entities.ConstructionSite>();

            CreateMap<GetAllConstructionSiteQuery, ConstructionSiteDTO>();
            CreateMap<GetConstructionByIdQuery, ConstructionSiteDTO>();
            CreateMap<GetConstructionByNameQuery, ConstructionSiteDTO>();

            CreateMap<DeleteConstructionSiteCommand, Domain.Entities.ConstructionSite>();

        }

    }
}
