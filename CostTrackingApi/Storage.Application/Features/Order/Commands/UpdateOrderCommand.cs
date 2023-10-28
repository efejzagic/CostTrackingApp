﻿using AutoMapper;
using MediatR;
using Storage.Application.DTOs.Order;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Application.Features.Order.Commands
{
    public class UpdateOrderCommand : IRequest<Response<string>>
    {
        //public int Id { get; set; }
        public EditOrderDTO Value { get; set; }
        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Domain.Entities.Order> _Repository;
            private readonly IMapper _mapper;
            public UpdateOrderCommandHandler(IGenericRepositoryAsync<Domain.Entities.Order> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Domain.Entities.Order>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
