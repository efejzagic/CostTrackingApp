using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.Invoice.Commands
{
    public class DeleteInvoiceCommand : IRequest<Wrappers.Response<string>>
    {
        public int Id { get; set; }
        public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Wrappers.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Invoice> _Repository;
            private readonly IMapper _mapper;
            public DeleteInvoiceCommandHandler (IGenericRepositoryAsync<Domain.Entities.Invoice> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Wrappers.Response<string>> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Invoice>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Wrappers.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
