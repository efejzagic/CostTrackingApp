﻿using Equipment.Application.DTOs.ToolService;
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

namespace Equipment.Application.Features.ToolService.Queries
{
    public class GetToolServiceByIdQuery : IRequest<Response<ToolServiceDTO>>
    {
        public int Id { get; set; }
    }
    public class GetToolServiceByIdQueryHandler: IRequestHandler<GetToolServiceByIdQuery, Response<ToolServiceDTO>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> _repository;
        private readonly IMapper _mapper;
        public GetToolServiceByIdQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ToolServiceDTO>> Handle(GetToolServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ToolServiceDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<ToolServiceDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ToolServiceDTO>(enviroment);
            return new Response<ToolServiceDTO>(enviromentViewModel);
        }
    }
}