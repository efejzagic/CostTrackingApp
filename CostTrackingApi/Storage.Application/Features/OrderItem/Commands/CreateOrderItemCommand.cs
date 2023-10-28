
using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Article;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CorrelationIdLibrary.Interfaces;
using Storage.Application.DTOs.OrderItem;

namespace Storage.Application.Features.OrderItem.Commands
{
    public partial class CreateOrderItemCommand : IRequest<Response<string>>
    {
        public CreateOrderItemDTO Value { get; set; }

    }
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Response<string>>
    {

        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.OrderItem> _Repository;
        private readonly IMapper _mapper;
        public CreateOrderItemCommandHandler(IGenericRepositoryAsync<Storage.Domain.Entities.OrderItem> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Storage.Domain.Entities.OrderItem>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
