using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maintenance.Application.DTOs.MaintenanceRecord;
using Maintenance.Application.Interfaces;

namespace Maintenance.Application.Features.MaintenanceRecord.Commands
{
    public class UpdateMaintenanceRecordCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public MaintenanceRecordEditDTO Value { get; set; }
        public class UpdateMaintenanceRecordCommandHandler : IRequestHandler<UpdateMaintenanceRecordCommand, ResponseInfo.Entities.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _Repository;
            private readonly IMapper _mapper;
            public UpdateMaintenanceRecordCommandHandler (IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<ResponseInfo.Entities.Response<string>> Handle(UpdateMaintenanceRecordCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Maintenance.Domain.Entities.MaintenanceRecord>(request.Value);
                await _Repository.UpdateAsync(enviroment);
                return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
