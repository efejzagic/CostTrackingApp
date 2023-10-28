using AutoMapper;
using MediatR;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Order.Commands
{
    public class DeleteOrderCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Order> _Repository;
            private readonly IMapper _mapper;
            public DeleteOrderCommandHandler(IGenericRepositoryAsync<Domain.Entities.Order> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Order>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
