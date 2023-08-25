using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DTOs.Expense;
using Finance.Application.Interfaces;
using MediatR;


namespace Finance.Application.Features.Expense.Commands
{
    public class CreateExpenseCommand : IRequest<Wrappers.Response<string>>
    {
        public CreateExpenseDTO Value { get; set; }
    }

    public class CreateExpenseCommandHandler: IRequestHandler<CreateExpenseCommand, Wrappers.Response<string>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Expense> _Repository;
        private readonly IMapper _mapper;
        public CreateExpenseCommandHandler (IGenericRepositoryAsync<Domain.Entities.Expense> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Wrappers.Response<string>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Domain.Entities.Expense>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Wrappers.Response<string>(enviroment.Id.ToString());
        }
    }
}
