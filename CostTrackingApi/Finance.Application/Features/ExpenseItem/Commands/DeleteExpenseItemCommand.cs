using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.ExpenseItem.Commands
{
    public class DeleteExpenseItemCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public int Id { get; set; }
        public class DeleteExpenseItemCommandHandler : IRequestHandler<DeleteExpenseItemCommand, ResponseInfo.Entities.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.ExpenseItem> _Repository;
            private readonly IMapper _mapper;
            public DeleteExpenseItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.ExpenseItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<ResponseInfo.Entities.Response<string>> Handle(DeleteExpenseItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.ExpenseItem>(request);
                await _Repository.DeleteAsync(enviroment);
                return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
