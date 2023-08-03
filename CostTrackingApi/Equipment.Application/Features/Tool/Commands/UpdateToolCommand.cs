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
using Equipment.Application.DTOs.Tool;

namespace Equipment.Application.Features.Tool.Commands
{
    public class UpdateToolCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public ToolEditDTO Value { get; set; }
        public class UpdateToolCommandHandler : IRequestHandler<UpdateToolCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> _Repository;
            private readonly IMapper _mapper;
            public UpdateToolCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateToolCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.Tool>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
