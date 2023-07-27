using Equipment.Application.DTOs.Machinery;
using Equipment.Application.Interfaces;
//using Equipment.Application.Parameters.Enviroment;
using Equipment.Application.Wrappers;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.Application.DTOs.ToolService;

namespace Equipment.Application.Features.ToolService.Queries
{
    public class GetAllToolServiceQuery : IRequest<PagedResponse<IEnumerable<ToolServiceDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        //public List<ToolServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllToolServiceQueryHandler: IRequestHandler<GetAllToolServiceQuery, PagedResponse<IEnumerable<ToolServiceDTO>>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> _repository;
        private readonly IMapper _mapper;
        public GetAllToolServiceQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ToolServiceDTO>>> Handle(GetAllToolServiceQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ToolServiceDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<ToolServiceDTO>>(enviroments);
            return new PagedResponse<IEnumerable<ToolServiceDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}