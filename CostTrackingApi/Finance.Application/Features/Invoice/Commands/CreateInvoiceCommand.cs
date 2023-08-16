using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Interfaces;
using MediatR;


namespace Finance.Application.Features.Invoice.Commands
{
    public class CreateInvoiceCommand : IRequest<Wrappers.Response<string>>
    {
        public CreateInvoiceDTO Value { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Wrappers.Response<string>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _Repository;
        private readonly IMapper _mapper;
        public CreateInvoiceCommandHandler (IGenericRepositoryAsync<Domain.Entities.Invoice> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Wrappers.Response<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Domain.Entities.Invoice>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Wrappers.Response<string>(enviroment.Id.ToString());
        }
    }
}
