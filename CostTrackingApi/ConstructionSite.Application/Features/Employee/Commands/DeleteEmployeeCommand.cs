using AutoMapper;
using MediatR;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Wrappers;
using ConstructionSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.Employee.Commands
{
    public class DeleteEmployeeCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Employee> _Repository;
            private readonly IMapper _mapper;
            public DeleteEmployeeCommandHandler(IGenericRepositoryAsync<Domain.Entities.Employee> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Employee>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
