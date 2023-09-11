using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.ExpenseItem;
using ResponseInfo.Entities;
using Finance.Application.DTOs.ExpenseItem;

namespace Finance.Application.Features.ExpenseItem.Queries
{
    public class GetAllExpenseItemsQuery : IRequest<PagedResponse<IEnumerable<ExpenseItemDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllExpenseItemsQueryHandler : IRequestHandler<GetAllExpenseItemsQuery, PagedResponse<IEnumerable<ExpenseItemDTO>>>
    {
        
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.ExpenseItem> _repository;
        private readonly IMapper _mapper;
        public GetAllExpenseItemsQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.ExpenseItem> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ExpenseItemDTO>>> Handle(GetAllExpenseItemsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ExpenseItemDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<ExpenseItemDTO>>(enviroments);
            return new PagedResponse<IEnumerable<ExpenseItemDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}