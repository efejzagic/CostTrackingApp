using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.DTOs.Employee;
using ConstructionSite.Application.Interfaces;
using MediatR;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.Employee.Queries
{
    public class GetEmployeeByIdQuery : IRequest<Response<EmployeeDTO>>
    {
        public int Id { get; set; }
    }
    public class GetEmployeeByIdQueryHandler: IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Employee> _repository;
        private readonly IMapper _mapper;
        public GetEmployeeByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<EmployeeDTO>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<EmployeeDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<EmployeeDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<EmployeeDTO>(enviroment);
            return new Response<EmployeeDTO>(enviromentViewModel);
        }
    }
}
