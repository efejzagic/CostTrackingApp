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

    public class CreateInvoiceCommand : IRequest<Wrappers.Response<InvoiceDTO>>
    {
        public CreateInvoiceDTO Value { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Wrappers.Response<InvoiceDTO>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _Repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator; // Add this

        public CreateInvoiceCommandHandler (IGenericRepositoryAsync<Domain.Entities.Invoice> Repository, IMapper mapper, IMediator mediator)
        {
            _Repository = Repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Wrappers.Response<InvoiceDTO>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {


            // Handle invoiceItemResponses as needed

            var environment = _mapper.Map<Domain.Entities.Invoice>(request.Value);
            await _Repository.AddAsync(environment);
            var enviromentViewModel = _mapper.Map<InvoiceDTO>(environment);
            return new Wrappers.Response<InvoiceDTO>(enviromentViewModel);


            //var invoiceEntity = new Domain.Entities.Invoice
            //{
            //    Date = request.Value.Date,
            //    DueDate = request.Value.DueDate,
            //    Amount = request.Value.Amount,
            //    ConstructionSiteId = request.Value.ConstructionSiteId,
            //    MachineryId = request.Value.MachineryId,
            //    ToolId = request.Value.ToolId,
            //    MaintenanceRecordId = request.Value.MaintenanceRecordId,
            //    ArticleId = request.Value.ArticleId
            //};

            //// Create the invoice entity
            //await _Repository.AddAsync(invoiceEntity);

            //// Placeholder list of InvoiceItems to be populated later
            //var invoiceItemsToCreate = new List<CreateInvoiceItemDTO>();

            //// Create the DTOs for InvoiceItems and populate the placeholder list
            //foreach (var itemDto in request.Value.Items)
            //{
            //    var invoiceItemEntity = new Domain.Entities.InvoiceItem
            //    {
            //        Description = itemDto.Description,
            //        Amount = itemDto.Amount,
            //        InvoiceId = invoiceEntity.Id // Assign InvoiceId
            //    };

            //    invoiceItemsToCreate.Add(new CreateInvoiceItemDTO
            //    {
            //        Description = itemDto.Description,
            //        Amount = itemDto.Amount
            //    });
            //}

            //// Dispatch CreateInvoiceItemCommands for each placeholder DTO
            //foreach (var itemToCreate in invoiceItemsToCreate)
            //{
            //    var createItemCommand = new CreateInvoiceItemCommand { Value = itemToCreate };
            //    await _mediator.Send(createItemCommand);
            //}

            //return new Wrappers.Response<string>(invoiceEntity.Id.ToString());


        }
    }
}
