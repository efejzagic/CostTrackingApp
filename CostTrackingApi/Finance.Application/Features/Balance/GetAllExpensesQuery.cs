using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Finance.Application.DTOs.Expense;
using Finance.Application.Interfaces;
using ResponseInfo.Entities;
using Finance.Application.Parameters.Expense;

namespace Finance.Application.Features.Balance
{
    public class GetBalanceQuery : IRequest<object>
    {

    }
    public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, object>
    {
        
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Expense> _expenseRepo;
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _invoiceRepo;
        public GetBalanceQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.Expense> expenseRepo,
            IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> invoiceRepo)
        {
            _expenseRepo = expenseRepo;
            _invoiceRepo = invoiceRepo;
        }

        public async Task<object> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepo.GetAllAsync();
            var invoices = await _invoiceRepo.GetAllAsync();

            double expenseTotal = 0.0;
            double invoiceTotal = 0.0;

            foreach (var item in expenses)
            {
                expenseTotal += (double)item.Amount;
            }
            
            foreach (var item in invoices)
            {
                invoiceTotal += (double)item.Amount;
            }

            return new
            {
                Balance = invoiceTotal - expenseTotal,
                Expenses = expenseTotal,
                Incomes = invoiceTotal
            };
        }
    }
}