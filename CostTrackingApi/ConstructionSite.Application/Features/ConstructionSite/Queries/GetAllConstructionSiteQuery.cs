using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Parameters.ConstructionSite;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.ConstructionSite.Queries
{
    public class GetAllConstructionSiteQuery : IRequest<PagedResponse<IEnumerable<ConstructionSiteDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }
    }
    public class GetAllConstructionSiteQueryHandler : IRequestHandler<GetAllConstructionSiteQuery, PagedResponse<IEnumerable<ConstructionSiteDTO>>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _repository;
        private readonly IMapper _mapper;
        public GetAllConstructionSiteQueryHandler(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ConstructionSiteDTO>>> Handle(GetAllConstructionSiteQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllConstructionSiteParameter>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<ConstructionSiteDTO>>(enviroments);
            return new PagedResponse<IEnumerable<ConstructionSiteDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
