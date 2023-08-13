using AutoMapper;
using MediatR;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Domain.Entities;
using ConstructionSite.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConstructionSite.Application.DTOs.ConstructionSite;

namespace ConstructionSite.Application.Features.ConstructionSite.Commands
{
    public class UpdateConstructionSiteCommand : IRequest<Response<string>>
    {
        //public int Id { get; set; }
        public ConstructionSiteEditDTO Value { get; set; }
        public class UpdateConstructionSiteCommandHandler : IRequestHandler<UpdateConstructionSiteCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _Repository;
            private readonly IMapper _mapper;
            public UpdateConstructionSiteCommandHandler(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateConstructionSiteCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.ConstructionSite>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
