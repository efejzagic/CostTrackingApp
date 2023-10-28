using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.ExpenseItem;
using ResponseInfo.Entities;


namespace Finance.Application.Features.ExpenseItem.Commands
{
    public class UpdateExpenseItemCommand : IRequest<Response<string>>
    {
        public EditExpenseItemDTO Value { get; set; }
        public class UpdateExpenseItemCommandHandler : IRequestHandler<UpdateExpenseItemCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.ExpenseItem> _Repository;
            private readonly IMapper _mapper;
            public UpdateExpenseItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.ExpenseItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateExpenseItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.ExpenseItem>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
