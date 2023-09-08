using AutoMapper;
using ConstructionSite.Application.DTOs.Employee;
using ConstructionSite.Application.Features.Employee.Commands;
using ConstructionSite.Application.Features.Employee.Queries;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Parameters.Employee;
using ConstructionSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _repository;
        public EmployeeProfile(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> repository)
        {
            _repository = repository;

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(d => d.ConstructionSite, opt => opt.MapFrom(src => _repository.GetByIdAsync(src.ConstructionSiteId).Result));

            CreateMap<Employee, EmployeeConstructionSiteDTO>();
            CreateMap<EmployeeEditDTO, Employee>();
            CreateMap<EmployeeCreateDTO, Employee>();

            CreateMap<GetAllEmployeeQuery, EmployeeDTO>();
            CreateMap<GetEmployeeByIdQuery, EmployeeDTO>();
            CreateMap<GetEmployeeByNameQuery, EmployeeDTO>();

            CreateMap<DeleteEmployeeCommand, Employee>();

            CreateMap<GetAllEmployeeQuery, GetAllEmployeeParameter>();
        }

    }
}
