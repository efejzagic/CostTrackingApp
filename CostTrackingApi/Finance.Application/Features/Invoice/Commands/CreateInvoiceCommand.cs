using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DTOs.Invoice;
using Finance.Application.DTOs.InvoiceItem;
using Finance.Application.Features.InvoiceItem.Commands;
using Finance.Application.Interfaces;
using Finance.Domain.Entities;
using MediatR;


namespace Finance.Application.Features.Invoice.Commands
{

    public class CreateInvoiceCommand : IRequest<ResponseInfo.Entities.Response<InvoiceDTO>>
    {
        public CreateInvoiceDTO Value { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, ResponseInfo.Entities.Response<InvoiceDTO>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _Repository;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler (IGenericRepositoryAsync<Domain.Entities.Invoice> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<InvoiceDTO>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        { 
            foreach(var item in request.Value.Items)
            {
                request.Value.Amount += item.Amount;
            }
            // Handle invoiceItemResponses as 
            var environment = _mapper.Map<Domain.Entities.Invoice>(request.Value);
            await _Repository.AddAsync(environment);
            var enviromentViewModel = _mapper.Map<InvoiceDTO>(environment);
            return new ResponseInfo.Entities.Response<InvoiceDTO>(enviromentViewModel);
        }
    }
}
