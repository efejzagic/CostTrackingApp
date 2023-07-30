using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.DTOs.Employee;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.Employee.Queries
{
    public class GetEmployeeByNameQuery : IRequest<Response<EmployeeDTO>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class GetEmployeeByNameQueryHandler : IRequestHandler<GetEmployeeByNameQuery, Response<EmployeeDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Employee> _repository;
        private readonly IMapper _mapper;
        public GetEmployeeByNameQueryHandler(IGenericRepositoryAsync<Domain.Entities.Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<EmployeeDTO>> Handle(GetEmployeeByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<EmployeeDTO>(request);
            var enviroment = await _repository.GetByConditionAsync(cs => cs.Name == request.Name && cs.Surname == request.Surname);
            if (enviroment == null)
            {
                return new Response<EmployeeDTO>()
                {
                    Succeeded = false,
                    Message = $"No construction site found in db with name = {request.Name} {request.Surname} ",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<EmployeeDTO>(enviroment);
            return new Response<EmployeeDTO>(enviromentViewModel);
        }
    }
}
