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
    public class DeleteInvoiceItemCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public int Id { get; set; }
        public class DeleteInvoiceItemCommandHandler : IRequestHandler<DeleteInvoiceItemCommand, ResponseInfo.Entities.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.InvoiceItem> _Repository;
            private readonly IMapper _mapper;
            public DeleteInvoiceItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.InvoiceItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<ResponseInfo.Entities.Response<string>> Handle(DeleteInvoiceItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.InvoiceItem>(request);
                await _Repository.DeleteAsync(enviroment);
                return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
