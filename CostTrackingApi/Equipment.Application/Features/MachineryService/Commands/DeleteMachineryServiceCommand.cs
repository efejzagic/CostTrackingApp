using Equipment.Application.Interfaces;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.Application.Features.MachineryService.Commands
{
    public class DeleteMachineryServiceCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteMachineryServiceCommandHandler : IRequestHandler<DeleteMachineryServiceCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> _Repository;
            private readonly IMapper _mapper;
            public DeleteMachineryServiceCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteMachineryServiceCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.MachineryService>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
