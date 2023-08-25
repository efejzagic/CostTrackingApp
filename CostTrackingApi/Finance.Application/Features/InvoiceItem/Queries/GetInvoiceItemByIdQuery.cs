using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Interfaces;
using Finance.Application.DTOs.InvoiceItem;

namespace Finance.Application.Features.InvoiceItem.Queries
{
    public class GetInvoiceItemByIdQuery : IRequest<Wrappers.Response<InvoiceItemDTO>>
    {
        public int Id { get; set; }
    }
    public class GetInvoiceItemByIdQueryHandler : IRequestHandler<GetInvoiceItemByIdQuery, Wrappers.Response<InvoiceItemDTO>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.InvoiceItem> _repository;
        private readonly IMapper _mapper;
        public GetInvoiceItemByIdQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.InvoiceItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Wrappers.Response<InvoiceItemDTO>> Handle(GetInvoiceItemByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<InvoiceItemDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Wrappers.Response<InvoiceItemDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<InvoiceItemDTO>(enviroment);
            return new Wrappers.Response<InvoiceItemDTO>(enviromentViewModel);
        }
    }
}
