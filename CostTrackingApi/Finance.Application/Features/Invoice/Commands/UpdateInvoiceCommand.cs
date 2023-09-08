using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Interfaces;
using ResponseInfo.Entities;

namespace Finance.Application.Features.Invoice.Commands
{
    public class UpdateInvoiceCommand : IRequest<Response<string>>
    {
        public EditInvoiceDTO Value { get; set; }
        public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Invoice> _Repository;
            private readonly IMapper _mapper;
            public UpdateInvoiceCommandHandler(IGenericRepositoryAsync<Domain.Entities.Invoice> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Invoice>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
