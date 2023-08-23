using AutoMapper;
using Finance.Application.DTOs.InvoiceItem;
using Finance.Application.Features.InvoiceItem.Commands;
using Finance.Application.Features.InvoiceItem.Queries;
using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Mappings
{
    public class InvoiceItemProfile : Profile
    {
        public InvoiceItemProfile()
        {
     //       CreateMap<GetAllInvoiceItemsQuery, InvoiceItemDTO>();
     //       CreateMap<GetInvoiceItemByIdQuery, InvoiceItemDTO>();
     //       CreateMap<CreateInvoiceItemDTO, Domain.Entities.InvoiceItem>()
     //.ForMember(dest => dest.Invoice, opt => opt.Ignore());
     //       CreateMap<EditInvoiceItemDTO, Domain.Entities.InvoiceItem>()
     //            .ForMember(dest => dest.Invoice, opt => opt.Ignore());
     //       CreateMap<DeleteInvoiceItemCommand, Domain.Entities.InvoiceItem>();
     //       CreateMap<Domain.Entities.InvoiceItem, InvoiceItemDTO>();
     //       CreateMap<CreateInvoiceItemDTO, Domain.Entities.InvoiceItem>();

        }
    }
}
