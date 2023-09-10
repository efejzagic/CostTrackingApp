using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.Expense.Commands
{
    public class DeleteExpenseCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public int Id { get; set; }
        public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, ResponseInfo.Entities.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Expense> _Repository;
            private readonly IMapper _mapper;
            public DeleteExpenseCommandHandler(IGenericRepositoryAsync<Domain.Entities.Expense> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<ResponseInfo.Entities.Response<string>> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Expense>(request);
                await _Repository.DeleteAsync(enviroment);
                return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
