using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Interfaces;
using MediatR;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.ConstructionSite.Queries
{
    public class GetConstructionByNameQuery : IRequest<Response<ConstructionSiteDTO>>
    {
        public string Title { get; set; }
    }
    public class GetConstructionByNameQueryHandler : IRequestHandler<GetConstructionByNameQuery, Response<ConstructionSiteDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _repository;
        private readonly IMapper _mapper;
        public GetConstructionByNameQueryHandler(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ConstructionSiteDTO>> Handle(GetConstructionByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ConstructionSiteDTO>(request);
            var enviroment = await _repository.GetByConditionAsync(cs => cs.Title == request.Title);
            if (enviroment == null)
            {
                return new Response<ConstructionSiteDTO>()
                {
                    Succeeded = false,
                    Message = $"No construction site found in db with title = {request.Title}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ConstructionSiteDTO>(enviroment);
            return new Response<ConstructionSiteDTO>(enviromentViewModel);
        }
    }
}
