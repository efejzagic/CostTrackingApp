using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;
using Storage.Application.DTOs.OrderItem;
using Storage.Application.Interfaces;
using Storage.Application.Parameters.OrderItem;

namespace Storage.Application.Features.OrderItem.Queries
{
    public class GetAllOrderItemQuery : IRequest<PagedResponse<IEnumerable<OrderItemDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        //public List<MachineryServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllOrderItemQueryHandler : IRequestHandler<GetAllOrderItemQuery, PagedResponse<IEnumerable<OrderItemDTO>>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.OrderItem> _repository;
        private readonly IMapper _mapper;
        public GetAllOrderItemQueryHandler(IGenericRepositoryAsync<Domain.Entities.OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<OrderItemDTO>>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllOrderItemParameter>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<OrderItemDTO>>(enviroments);
            return new PagedResponse<IEnumerable<OrderItemDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}