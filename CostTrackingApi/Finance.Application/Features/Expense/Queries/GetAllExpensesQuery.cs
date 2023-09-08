using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Finance.Application.DTOs.Expense;
using Finance.Application.Interfaces;
using ResponseInfo.Entities;
using Finance.Application.Parameters.Expense;

namespace Finance.Application.Features.Expense.Queries
{
    public class GetAllExpensesQuery : IRequest<PagedResponse<IEnumerable<ExpenseDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, PagedResponse<IEnumerable<ExpenseDTO>>>
    {
        
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Expense> _repository;
        private readonly IMapper _mapper;
        public GetAllExpensesQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.Expense> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ExpenseDTO>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllExpenseParameter>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<ExpenseDTO>>(enviroments);
            return new PagedResponse<IEnumerable<ExpenseDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}