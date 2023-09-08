using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Maintenance.Application.DTOs.MaintenanceRecord;
using Maintenance.Application.Interfaces;
using MediatR; 


namespace Maintenance.Application.Features.MaintenanceRecord.Commands
{
    public class CreateMaintenanceRecordCommand : IRequest<ResponseInfo.Entities.Response<string>>
    {
        public MaintenanceRecordCreateDTO Value { get; set; }
    }

    public class CreateMaintenanceRecordCommandHandler : IRequestHandler<CreateMaintenanceRecordCommand, ResponseInfo.Entities.Response<string>>
    {
        private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _Repository;
        private readonly IMapper _mapper;
        public CreateMaintenanceRecordCommandHandler(IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<ResponseInfo.Entities.Response<string>> Handle(CreateMaintenanceRecordCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Maintenance.Domain.Entities.MaintenanceRecord>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
        }
    }
}
