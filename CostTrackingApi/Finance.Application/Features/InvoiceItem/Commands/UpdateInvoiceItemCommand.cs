using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.InvoiceItem;
using ResponseInfo.Entities;


namespace Finance.Application.Features.InvoiceItem.Commands
{
    public class UpdateInvoiceItemCommand : IRequest<Response<string>>
    {
        public EditInvoiceItemDTO Value { get; set; }
        public class UpdateInvoiceItemCommandHandler : IRequestHandler<UpdateInvoiceItemCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.InvoiceItem> _Repository;
            private readonly IMapper _mapper;
            public UpdateInvoiceItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.InvoiceItem> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateInvoiceItemCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.InvoiceItem>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
