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

namespace Equipment.Application.Features.ToolService.Commands
{
    public class DeleteToolServiceCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteToolServiceCommandHandler : IRequestHandler<DeleteToolServiceCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> _Repository;
            private readonly IMapper _mapper;
            public DeleteToolServiceCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteToolServiceCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.ToolService>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
