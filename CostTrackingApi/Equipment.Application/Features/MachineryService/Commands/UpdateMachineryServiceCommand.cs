using Equipment.Application.Interfaces;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.Application.DTOs.Machinery;
using Equipment.Application.DTOs.MachineryService;

namespace Equipment.Application.Features.MachineryService.Commands
{
    public class UpdateMachineryServiceCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public MachineryServiceEditDTO Value { get; set; }
        public class UpdateMachineryServiceCommandHandler : IRequestHandler<UpdateMachineryServiceCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> _Repository;
            private readonly IMapper _mapper;
            public UpdateMachineryServiceCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateMachineryServiceCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.MachineryService>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
