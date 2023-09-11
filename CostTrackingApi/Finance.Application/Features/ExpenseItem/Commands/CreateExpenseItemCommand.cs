using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DTOs.ExpenseItem;
using Finance.Application.Interfaces;
using MediatR;


namespace Finance.Application.Features.ExpenseItem.Commands
{
    public class CreateExpenseItemCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public CreateExpenseItemDTO Value { get; set; }
    }

    public class CreateExpenseItemCommandHandler : IRequestHandler<CreateExpenseItemCommand, ResponseInfo.Entities.Response<string>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.ExpenseItem> _Repository;
        private readonly IMapper _mapper;
        public CreateExpenseItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.ExpenseItem> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<string>> Handle(CreateExpenseItemCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Domain.Entities.ExpenseItem>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
        }
    }
}
