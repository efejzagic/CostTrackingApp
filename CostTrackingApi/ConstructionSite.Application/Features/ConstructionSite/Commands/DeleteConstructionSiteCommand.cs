using AutoMapper;
using MediatR;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;

namespace ConstructionSite.Application.Features.ConstructionSite.Commands
{
    public class DeleteConstructionSiteCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteConstructionSiteCommandHandler : IRequestHandler<DeleteConstructionSiteCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _Repository;
            private readonly IMapper _mapper;
            public DeleteConstructionSiteCommandHandler(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteConstructionSiteCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.ConstructionSite>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
