using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Finance.Application.DTOs.Invoice;
using Finance.Application.Interfaces;

namespace Finance.Application.Features.Invoice.Queries
{
    public class GetInvoiceByIdQuery : IRequest<ResponseInfo.Entities.Response<InvoiceDTO>>
    {
        public int Id { get; set; }
    }
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, ResponseInfo.Entities.Response<InvoiceDTO>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> _repository;
        private readonly IMapper _mapper;
        public GetInvoiceByIdQueryHandler(IGenericRepositoryAsync<Finance.Domain.Entities.Invoice> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<InvoiceDTO>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<InvoiceDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new ResponseInfo.Entities.Response<InvoiceDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<InvoiceDTO>(enviroment);
            return new ResponseInfo.Entities.Response<InvoiceDTO>(enviromentViewModel);
        }
    }
}
