using Equipment.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.Application.DTOs.Tool;
using ResponseInfo.Entities;
using Equipment.Application.Parameters.Tool;

namespace Equipment.Application.Features.Tool.Queries
{
    public class GetAllToolQuery : IRequest<PagedResponse<IEnumerable<ToolDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllToolQueryHandler : IRequestHandler<GetAllToolQuery, PagedResponse<IEnumerable<ToolDTO>>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> _repository;
        private readonly IMapper _mapper;
        public GetAllToolQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ToolDTO>>> Handle(GetAllToolQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllToolParameter>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<ToolDTO>>(enviroments);
            return new PagedResponse<IEnumerable<ToolDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}