using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.InvoiceItem.Commands
{
    public class DeleteInvoiceItemCommand : IRequest<Wrappers.Response<string>>
    {
        public int Id { get; set; }
        public class DeleteInvoiceItemCommandHandler : IRequestHandler<DeleteInvoiceItemCommand, Wrappers.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.InvoiceItem> _Repository;
            private readonly IMapper _mapper;
            public DeleteInvoiceItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.InvoiceItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Wrappers.Response<string>> Handle(DeleteInvoiceItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.InvoiceItem>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Wrappers.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
