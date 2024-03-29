﻿using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Supplier;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Supplier.Commands
{
    public class UpdateSupplierCommand : IRequest<Response<Domain.Entities.Supplier>>
    {
        //public int Id { get; set; }
        public SupplierEditDTO Value { get; set; }
        public class UpdateSupplierCommandHandler : IRequestHandler<UpdateSupplierCommand, Response<Domain.Entities.Supplier>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Supplier> _Repository;
            private readonly IMapper _mapper;
            public UpdateSupplierCommandHandler(IGenericRepositoryAsync<Domain.Entities.Supplier> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<Domain.Entities.Supplier>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Supplier>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<Domain.Entities.Supplier>(enviroment);
            }
        }
    }
}
