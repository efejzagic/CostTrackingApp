using AutoMapper;
using Finance.Application.DTOs.Invoice;
using Finance.Application.DTOs.InvoiceItem;
using Finance.Application.Features.Invoice.Commands;
using Finance.Application.Features.Invoice.Queries;
using Finance.Application.Features.InvoiceItem.Commands;
using Finance.Application.Features.InvoiceItem.Queries;
using Finance.Application.Interfaces;
using Finance.Application.Parameters.Invoice;
using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Mappings
{
    public class InvoiceProfile : Profile
    {
        private readonly IInvoiceRepository _invoiceRepo;
        public InvoiceProfile(IInvoiceRepository invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;

            CreateMap<GetAllInvoicesQuery, InvoiceDTO>();
            CreateMap<GetInvoiceByIdQuery, InvoiceDTO>();
            CreateMap<CreateInvoiceDTO, Invoice>();
            CreateMap<EditInvoiceDTO, Invoice>();
            CreateMap<DeleteInvoiceCommand, Invoice>();
            CreateMap<CreateInvoiceDTO, Invoice>();
            CreateMap<EditInvoiceDTO, Invoice>();
            CreateMap<Invoice, InvoiceDTO>()
              .PreserveReferences()
              .ForMember(d => d.Items, opt => opt.MapFrom(src => _invoiceRepo.GetItemsByInvoiceId(src.Id).Result));
            CreateMap<Invoice, GetAllInvoiceParameter>();
            CreateMap<GetAllInvoicesQuery, GetAllInvoiceParameter>();


            CreateMap<GetAllInvoiceItemsQuery, InvoiceItemDTO>();
            CreateMap<GetInvoiceItemByIdQuery, InvoiceItemDTO>();
            CreateMap<CreateInvoiceItemDTO, Domain.Entities.InvoiceItem>()
     .ForMember(dest => dest.Invoice, opt => opt.Ignore());
            CreateMap<EditInvoiceItemDTO, Domain.Entities.InvoiceItem>()
                 .ForMember(dest => dest.Invoice, opt => opt.Ignore());
            CreateMap<DeleteInvoiceItemCommand, Domain.Entities.InvoiceItem>();
            CreateMap<Domain.Entities.InvoiceItem, InvoiceItemDTO>();
            CreateMap<CreateInvoiceItemDTO, Domain.Entities.InvoiceItem>();



        }
    }
}
