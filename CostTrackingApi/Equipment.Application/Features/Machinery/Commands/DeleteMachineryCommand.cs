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


namespace Equipment.Application.Features.Machinery.Commands
{
    public class DeleteMachineryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteEnviromentCommandHandler : IRequestHandler<DeleteMachineryCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> _Repository;
            private readonly IMapper _mapper;
            public DeleteEnviromentCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteMachineryCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Equipment.Domain.Entities.Machinery>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
