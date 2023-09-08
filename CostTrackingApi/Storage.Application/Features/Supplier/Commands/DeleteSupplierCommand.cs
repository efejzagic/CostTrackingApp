using AutoMapper;
using MediatR;
using Storage.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;


namespace Storage.Application.Features.Supplier.Commands
{
    public class DeleteSupplierCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Supplier> _Repository;
            private readonly IMapper _mapper;
            public DeleteSupplierCommandHandler(IGenericRepositoryAsync<Domain.Entities.Supplier> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Supplier>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
