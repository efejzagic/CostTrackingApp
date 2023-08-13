using AutoMapper;
using MediatR;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Domain.Entities;
using ConstructionSite.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.DTOs.Employee;

namespace ConstructionSite.Application.Features.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest<Response<string>>
    {
        //public int Id { get; set; }
        public EmployeeEditDTO Value { get; set; }
        public class UpdateEmployeeCommandHandler: IRequestHandler<UpdateEmployeeCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Employee> _Repository;
            private readonly IMapper _mapper;
            public UpdateEmployeeCommandHandler(IGenericRepositoryAsync<Domain.Entities.Employee> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Employee>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
