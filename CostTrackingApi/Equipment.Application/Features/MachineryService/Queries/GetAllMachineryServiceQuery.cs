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
using Equipment.Application.DTOs.MachineryService;

namespace Equipment.Application.Features.MachineryService.Queries
{
    public class GetAllMachineryServiceQuery : IRequest<PagedResponse<IEnumerable<MachineryServiceDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        //public List<MachineryServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllMachineryServiceQueryHandler: IRequestHandler<GetAllMachineryServiceQuery, PagedResponse<IEnumerable<MachineryServiceDTO>>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> _repository;
        private readonly IMapper _mapper;
        public GetAllMachineryServiceQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<MachineryServiceDTO>>> Handle(GetAllMachineryServiceQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MachineryServiceDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<MachineryServiceDTO>>(enviroments);
            return new PagedResponse<IEnumerable<MachineryServiceDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}