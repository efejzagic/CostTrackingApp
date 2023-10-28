using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;
using Storage.Application.DTOs.Order;
using Storage.Application.Interfaces;
using Storage.Application.Parameters.Order;

namespace Storage.Application.Features.Order.Queries
{
    public class GetAllOrderQuery : IRequest<PagedResponse<IEnumerable<OrderDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        //public List<MachineryServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, PagedResponse<IEnumerable<OrderDTO>>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Order> _repository;
        private readonly IMapper _mapper;
        public GetAllOrderQueryHandler(IGenericRepositoryAsync<Domain.Entities.Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<OrderDTO>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllOrderParameter>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<OrderDTO>>(enviroments);
            return new PagedResponse<IEnumerable<OrderDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}