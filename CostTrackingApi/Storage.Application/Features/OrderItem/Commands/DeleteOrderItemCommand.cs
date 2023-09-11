using AutoMapper;
using MediatR;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.OrderItem.Commands
{
    public class DeleteOrderItemCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.OrderItem> _Repository;
            private readonly IMapper _mapper;
            public DeleteOrderItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.OrderItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.OrderItem>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
