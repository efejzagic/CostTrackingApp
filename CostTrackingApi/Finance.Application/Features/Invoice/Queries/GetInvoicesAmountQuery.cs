using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.Invoice.Queries
{
    public class GetInvoicesAmountQuery : IRequest<double>
    {

        public IKey Key { get; set; }

    }
    public class GetInvoicesAmountQueryHandler : IRequestHandler<GetInvoicesAmountQuery, double>
    {
        
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _repository;
        private readonly IMapper _mapper;
        public GetInvoicesAmountQueryHandler (IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> repository, IMapper mapper)
        {
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<double> Handle(GetInvoicesAmountQuery request, CancellationToken cancellationToken)
        {
            var enviroments = await _repository.GetAllAsync();
            double totalAmount = 0.0;
            foreach (var item in enviroments)
            {
                totalAmount += (double)item.Amount;
            }
            return totalAmount;
        }
    }
}