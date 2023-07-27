
using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Supplier;
using Storage.Application.Interfaces;
using Storage.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Application.Features.Supplier.Commands
{
    public partial class CreateSupplierCommand : IRequest<Response<string>>
    {
        public SupplierCreateDTO Value { get; set; }

    }
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.Supplier> _Repository;
        private readonly IMapper _mapper;
        public CreateSupplierCommandHandler(IGenericRepositoryAsync<Storage.Domain.Entities.Supplier> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Storage.Domain.Entities.Supplier>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}