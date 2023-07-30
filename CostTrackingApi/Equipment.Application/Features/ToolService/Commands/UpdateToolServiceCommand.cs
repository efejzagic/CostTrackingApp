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
using Equipment.Application.DTOs.ToolService;

namespace Equipment.Application.Features.ToolService.Commands
{
    public class UpdateToolServiceCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public ToolServiceEditDTO Value { get; set; }
        public class UpdateToolServiceCommandHandler : IRequestHandler<UpdateToolServiceCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> _Repository;
            private readonly IMapper _mapper;
            public UpdateToolServiceCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateToolServiceCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.ToolService>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
