using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maintenance.Application.DTOs.MaintenanceRecord;
using Maintenance.Application.Interfaces;
using ResponseInfo.Entities;

namespace Maintenance.Application.Features.MaintenanceRecord.Commands
{
    public class UpdateMaintenanceRecordCommand : IRequest<Response<string>>
    {
        public MaintenanceRecordEditDTO Value { get; set; }
        public class UpdateMaintenanceRecordCommandHandler : IRequestHandler<UpdateMaintenanceRecordCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _Repository;
            private readonly IMapper _mapper;
            public UpdateMaintenanceRecordCommandHandler (IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(UpdateMaintenanceRecordCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Maintenance.Domain.Entities.MaintenanceRecord>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
