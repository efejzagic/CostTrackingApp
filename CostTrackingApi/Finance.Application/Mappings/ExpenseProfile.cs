using AutoMapper;
using Finance.Application.DTOs.Expense;
using Finance.Application.Features.Expense.Commands;
using Finance.Application.Features.Expense.Queries;
using Finance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Mappings
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<GetAllExpensesQuery, ExpenseDTO>();
            CreateMap<GetExpenseByIdQuery, ExpenseDTO>();
            CreateMap<CreateExpenseDTO, Expense>();
            CreateMap<EditExpenseDTO, Expense>();
            CreateMap<DeleteExpenseCommand, Expense>();
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<CreateExpenseDTO, Expense>();
            CreateMap<EditExpenseDTO, Expense>();

        }
    }
}
