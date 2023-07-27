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
using Equipment.Application.Parameters.Machinery;
using Equipment.Application.DTOs.MachineryService;

namespace Equipment.Application.Features.Machinery.Queries
{
    public class GetAllMachineryQuery : IRequest<PagedResponse<IEnumerable<MachineryDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        public List<MachineryServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllMachineryQueryHandler: IRequestHandler<GetAllMachineryQuery, PagedResponse<IEnumerable<MachineryDTO>>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> _repository;
        private readonly IMapper _mapper;
        public GetAllMachineryQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<MachineryDTO>>> Handle(GetAllMachineryQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MachineryDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<MachineryDTO>>(enviroments);
            return new PagedResponse<IEnumerable<MachineryDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}