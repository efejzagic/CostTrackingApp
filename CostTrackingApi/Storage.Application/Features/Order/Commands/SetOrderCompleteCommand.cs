using AutoMapper;
using MediatR;
using ResponseInfo.Entities;
using Storage.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Order.Commands
{
    public class SetOrderCompleteCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class SetOrderCompleteCommandHandler : IRequestHandler<SetOrderCompleteCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Order> _Repository;
            private readonly IMapper _mapper;
            public SetOrderCompleteCommandHandler(IGenericRepositoryAsync<Domain.Entities.Order> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(SetOrderCompleteCommand request, CancellationToken cancellationToken)
            {
                var order = await _Repository.GetByIdAsync(request.Id);
                order.OrderComplete = true;
                await _Repository.UpdateAsync(order);
                return new Response<string>(order.Id.ToString());
            }
        }
    }
}
