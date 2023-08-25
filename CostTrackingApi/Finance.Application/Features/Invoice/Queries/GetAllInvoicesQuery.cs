using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

using Finance.Application.Wrappers;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Interfaces;
using Finance.Application.Wrappers;

namespace Finance.Application.Features.Invoice.Queries
{
    public class GetAllInvoicesQuery : IRequest<PagedResponse<IEnumerable<InvoiceDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, PagedResponse<IEnumerable<InvoiceDTO>>>
    {
        
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _repository;
        private readonly IMapper _mapper;
        public GetAllInvoicesQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<InvoiceDTO>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<InvoiceDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<InvoiceDTO>>(enviroments);
            return new PagedResponse<IEnumerable<InvoiceDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}