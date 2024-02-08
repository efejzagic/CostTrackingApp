
using AutoMapper;
using MediatR;
using ResponseInfo.Entities;
using Storage.Application.DTOs.Supplier;
using Storage.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Storage.Application.Features.Supplier.Commands
{
    public partial class CreateSupplierCommand : IRequest<Response<Domain.Entities.Supplier>>
    {
        public SupplierCreateDTO Value { get; set; }

    }
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response<Domain.Entities.Supplier>>
    {
        private readonly IGenericRepositoryAsync<Storage.Domain.Entities.Supplier> _Repository;
        private readonly IMapper _mapper;
        public CreateSupplierCommandHandler(IGenericRepositoryAsync<Storage.Domain.Entities.Supplier> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<Domain.Entities.Supplier>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Storage.Domain.Entities.Supplier>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<Domain.Entities.Supplier>(enviroment);
        }
    }
}