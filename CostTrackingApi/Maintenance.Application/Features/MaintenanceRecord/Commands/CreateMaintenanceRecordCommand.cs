﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Maintenance.Application.DTOs.MaintenanceRecord;
using Maintenance.Application.Interfaces;
using MediatR;
using ResponseInfo.Entities;

namespace Maintenance.Application.Features.MaintenanceRecord.Commands
{
    public class CreateMaintenanceRecordCommand : IRequest<Response<string>>
    {
        public MaintenanceRecordCreateDTO Value { get; set; }
    }

    public class CreateMaintenanceRecordCommandHandler : IRequestHandler<CreateMaintenanceRecordCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _Repository;
        private readonly IMapper _mapper;
        public CreateMaintenanceRecordCommandHandler(IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Maintenance.Domain.Entities.MaintenanceRecord>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
