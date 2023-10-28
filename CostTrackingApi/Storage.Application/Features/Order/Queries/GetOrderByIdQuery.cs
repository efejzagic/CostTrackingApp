using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.DTOs.Order;
using AutoMapper;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;

namespace Storage.Application.Features.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<Response<OrderDTO>>
    {
        public int Id { get; set; }
    }
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Order> _repository;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.Order> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<OrderDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<OrderDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<OrderDTO>(enviroment);
            return new Response<OrderDTO>(enviromentViewModel);
        }
    }
}
