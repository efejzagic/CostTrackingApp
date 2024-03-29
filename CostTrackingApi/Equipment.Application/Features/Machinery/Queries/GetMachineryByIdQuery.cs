﻿using Equipment.Application.DTOs.Machinery;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.Application.Features.Machinery.Commands;
using ResponseInfo.Entities;


namespace Equipment.Application.Features.Machinery.Queries
{
    public class GetMachineryByIdQuery : IRequest<Response<MachineryDTO>>
    {
        public int Id { get; set; }
    }
    public class GetMachineryByIdQueryHandler: IRequestHandler<GetMachineryByIdQuery, Response<MachineryDTO>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> _repository;
        private readonly IMapper _mapper;
        public GetMachineryByIdQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<MachineryDTO>> Handle(GetMachineryByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MachineryDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<MachineryDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<MachineryDTO>(enviroment);
            return new Response<MachineryDTO>(enviromentViewModel);
        }
    }
}
