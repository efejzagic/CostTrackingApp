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

namespace Equipment.Application.Features.Machinery.Commands
{
    public class UpdateMachineryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public MachineryEditDTO Value { get; set; }
        public class UpdateMachineryCommandHandler : IRequestHandler<UpdateMachineryCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> _Repository;
            private readonly IMapper _mapper;
            public UpdateMachineryCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateMachineryCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.Machinery>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
