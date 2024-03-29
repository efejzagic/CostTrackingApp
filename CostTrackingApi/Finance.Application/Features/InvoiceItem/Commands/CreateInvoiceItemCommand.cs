﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Finance.Application.DTOs.InvoiceItem;
using Finance.Application.Interfaces;
using MediatR;


namespace Finance.Application.Features.InvoiceItem.Commands
{
    public class CreateInvoiceItemCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public CreateInvoiceItemDTO Value { get; set; }
    }

    public class CreateInvoiceItemCommandHandler : IRequestHandler<CreateInvoiceItemCommand, ResponseInfo.Entities.Response<string>>
    {
        private readonly IGenericRepositoryAsync<Finance.Domain.Entities.InvoiceItem> _Repository;
        private readonly IMapper _mapper;
        public CreateInvoiceItemCommandHandler(IGenericRepositoryAsync<Domain.Entities.InvoiceItem> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<string>> Handle(CreateInvoiceItemCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Domain.Entities.InvoiceItem>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
        }
    }
}
