using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Finance.Application.Wrappers;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.InvoiceItem;

namespace Finance.Application.Features.InvoiceItem.Queries
{
    public class GetAllInvoiceItemsQuery : IRequest<PagedResponse<IEnumerable<InvoiceItemDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllInvoiceItemsQueryHandler : IRequestHandler<GetAllInvoiceItemsQuery, PagedResponse<IEnumerable<InvoiceItemDTO>>>
    {
        
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.InvoiceItem> _repository;
        private readonly IMapper _mapper;
        public GetAllInvoiceItemsQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.InvoiceItem> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<InvoiceItemDTO>>> Handle(GetAllInvoiceItemsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<InvoiceItemDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<InvoiceItemDTO>>(enviroments);
            return new PagedResponse<IEnumerable<InvoiceItemDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}