using Equipment.Application.Interfaces;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;


namespace Equipment.Application.Features.Tool.Commands
{
    public class DeleteToolCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteToolCommandHandler : IRequestHandler<DeleteToolCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> _Repository;
            private readonly IMapper _mapper;
            public DeleteToolCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteToolCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map< Equipment.Domain.Entities.Tool> (request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
