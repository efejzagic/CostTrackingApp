using AutoMapper;
using Finance.Application.DTOs.ExpenseItem;
using Finance.Application.DTOs.Expense;
using Finance.Application.DTOs.ExpenseItem;
using Finance.Application.Features.Expense.Commands;
using Finance.Application.Features.Expense.Queries;
using Finance.Application.Features.ExpenseItem.Commands;
using Finance.Application.Features.ExpenseItem.Queries;
using Finance.Application.Interfaces;
using Finance.Application.Parameters.Expense;
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
        private readonly IExpenseRepository _ExpenseRepo;
        public ExpenseProfile(IExpenseRepository ExpenseRepo)
        {
            _ExpenseRepo = ExpenseRepo;

            CreateMap<GetAllExpensesQuery, ExpenseDTO>();
            CreateMap<GetExpenseByIdQuery, ExpenseDTO>();
            CreateMap<CreateExpenseDTO, Expense>();
            CreateMap<EditExpenseDTO, Expense>();
            CreateMap<DeleteExpenseCommand, Expense>();
            CreateMap<CreateExpenseDTO, Expense>();
            CreateMap<EditExpenseDTO, Expense>();
            CreateMap<Expense, ExpenseDTO>()
              .PreserveReferences()
              .ForMember(d => d.Items, opt => opt.MapFrom(src => _ExpenseRepo.GetItemsByExpenseId(src.Id).Result));
            CreateMap<Expense, GetAllExpenseParameter>();
            CreateMap<GetAllExpensesQuery, GetAllExpenseParameter>();


            CreateMap<GetAllExpenseItemsQuery, ExpenseItemDTO>();
            CreateMap<GetExpenseItemByIdQuery, ExpenseItemDTO>();
            CreateMap<CreateExpenseItemDTO, Domain.Entities.ExpenseItem>()
     .ForMember(dest => dest.Expense, opt => opt.Ignore());
            CreateMap<EditExpenseItemDTO, Domain.Entities.ExpenseItem>()
                 .ForMember(dest => dest.Expense, opt => opt.Ignore());
            CreateMap<DeleteExpenseItemCommand, Domain.Entities.ExpenseItem>();
            CreateMap<Domain.Entities.ExpenseItem, ExpenseItemDTO>();
            CreateMap<CreateExpenseItemDTO, Domain.Entities.ExpenseItem>();



        }
    }
}
