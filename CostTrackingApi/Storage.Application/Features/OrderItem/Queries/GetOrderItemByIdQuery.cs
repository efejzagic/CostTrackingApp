using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.DTOs.OrderItem;
using AutoMapper;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;

namespace Storage.Application.Features.OrderItem.Queries
{
    public class GetOrderItemByIdQuery : IRequest<Response<OrderItemDTO>>
    {
        public int Id { get; set; }
    }
    public class GetOrderItemByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, Response<OrderItemDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.OrderItem> _repository;
        private readonly IMapper _mapper;
        public GetOrderItemByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.OrderItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<OrderItemDTO>> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<OrderItemDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<OrderItemDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<OrderItemDTO>(enviroment);
            return new Response<OrderItemDTO>(enviromentViewModel);
        }
    }
}
