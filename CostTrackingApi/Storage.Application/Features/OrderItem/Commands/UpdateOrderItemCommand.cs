using AutoMapper;
using MediatR;
using Storage.Application.DTOs.OrderItem;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.OrderItem.Commands
{
    public class UpdateOrderItemCommand : IRequest<Response<string>>
    {
        //public int Id { get; set; }
        public EditOrderItemDTO Value { get; set; }
        public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.OrderItem> _Repository;
            private readonly IMapper _mapper;
            public UpdateOrderItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.OrderItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.OrderItem>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
