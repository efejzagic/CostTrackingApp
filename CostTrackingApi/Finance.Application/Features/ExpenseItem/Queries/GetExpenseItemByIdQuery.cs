using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.DTOs.Expense;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.ExpenseItem;
using Finance.Application.DTOs.ExpenseItem;

namespace Finance.Application.Features.ExpenseItem.Queries
{
    public class GetExpenseItemByIdQuery : IRequest<ResponseInfo.Entities.Response<ExpenseItemDTO>>
    {
        public int Id { get; set; }
    }
    public class GetExpenseItemByIdQueryHandler : IRequestHandler<GetExpenseItemByIdQuery, ResponseInfo.Entities.Response<ExpenseItemDTO>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.ExpenseItem> _repository;
        private readonly IMapper _mapper;
        public GetExpenseItemByIdQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.ExpenseItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<ExpenseItemDTO>> Handle(GetExpenseItemByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ExpenseItemDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new ResponseInfo.Entities.Response<ExpenseItemDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ExpenseItemDTO>(enviroment);
            return new ResponseInfo.Entities.Response<ExpenseItemDTO>(enviromentViewModel);
        }
    }
}
