using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maintenance.Application.Interfaces;

namespace Maintenance.Application.Features.MaintenanceRecord.Commands
{
    public class DeleteMaintenanceRecordCommand: IRequest<ResponseInfo.Entities.Response<string>>
    {
        public int Id { get; set; }
        public class DeleteMaintenanceRecordCommandHandler : IRequestHandler<DeleteMaintenanceRecordCommand, ResponseInfo.Entities.Response<string>>
        {
            private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _Repository;
            private readonly IMapper _mapper;
            public DeleteMaintenanceRecordCommandHandler(IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<ResponseInfo.Entities.Response<string>> Handle(DeleteMaintenanceRecordCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Maintenance.Domain.Entities.MaintenanceRecord>(request);
                await _Repository.DeleteAsync(enviroment);
                return new ResponseInfo.Entities.Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
