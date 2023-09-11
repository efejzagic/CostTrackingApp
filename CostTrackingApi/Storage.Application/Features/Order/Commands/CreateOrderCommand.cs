
using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Article;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CorrelationIdLibrary.Interfaces;
using Storage.Application.DTOs.Order;

namespace Storage.Application.Features.Order.Commands
{
    public partial class CreateOrderCommand : IRequest<Response<string>>
    {
        public CreateOrderDTO Value { get; set; }

    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<string>>
    {

        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.Order> _Repository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IGenericRepositoryAsync<Storage.Domain.Entities.Order> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Storage.Domain.Entities.Order>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
