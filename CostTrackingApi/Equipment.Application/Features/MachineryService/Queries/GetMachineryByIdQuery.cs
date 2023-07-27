﻿using Equipment.Application.DTOs.MachineryService;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.Application.Features.MachineryService.Queries
{
    public class GetMachineryServiceByIdQuery : IRequest<Response<MachineryServiceDTO>>
    {
        public int Id { get; set; }
    }
    public class GetMachineryServiceByIdQueryHandler: IRequestHandler<GetMachineryServiceByIdQuery, Response<MachineryServiceDTO>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> _repository;
        private readonly IMapper _mapper;
        public GetMachineryServiceByIdQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<MachineryServiceDTO>> Handle(GetMachineryServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MachineryServiceDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<MachineryServiceDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<MachineryServiceDTO>(enviroment);
            return new Response<MachineryServiceDTO>(enviromentViewModel);
        }
    }
}
