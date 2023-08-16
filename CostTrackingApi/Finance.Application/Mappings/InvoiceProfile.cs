using AutoMapper;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Features.Invoice.Commands;
using Finance.Application.Features.Invoice.Queries;
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
        public InvoiceProfile()
        {
            CreateMap<GetAllInvoicesQuery, InvoiceDTO>();
            CreateMap<GetInvoiceByIdQuery, InvoiceDTO>();
            CreateMap<CreateInvoiceDTO, Invoice>();
            CreateMap<EditInvoiceDTO, Invoice>();
            CreateMap<DeleteInvoiceCommand, Invoice>();
            CreateMap<Invoice, InvoiceDTO>();
            CreateMap<CreateInvoiceDTO, Invoice>();
            CreateMap<EditInvoiceDTO, Invoice>();
        }
    }
}
