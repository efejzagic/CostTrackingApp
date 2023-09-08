using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.Expense;
using ResponseInfo.Entities;

namespace Finance.Application.Features.Expense.Commands
{
    public class UpdateExpenseCommand : IRequest<Response<string>>
    {
        public EditExpenseDTO Value { get; set; }
        public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Expense> _Repository;
            private readonly IMapper _mapper;
            public UpdateExpenseCommandHandler (IGenericRepositoryAsync<Finance.Domain.Entities.Expense> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Finance.Domain.Entities.Expense>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
