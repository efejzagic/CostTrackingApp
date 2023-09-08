using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.DTOs.Expense;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.Expense.Queries
{
    public class GetExpenseByIdQuery : IRequest<ResponseInfo.Entities.Response<ExpenseDTO>>
    {
        public int Id { get; set; }
    }
    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, ResponseInfo.Entities.Response<ExpenseDTO>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Expense> _repository;
        private readonly IMapper _mapper;
        public GetExpenseByIdQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.Expense> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<ExpenseDTO>> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ExpenseDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new ResponseInfo.Entities.Response<ExpenseDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ExpenseDTO>(enviroment);
            return new ResponseInfo.Entities.Response<ExpenseDTO>(enviromentViewModel);
        }
    }
}
