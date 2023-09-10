using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maintenance.Application.Interfaces;
using ResponseInfo.Entities;


namespace Maintenance.Application.Features.MaintenanceRecord.Commands
{
    public class DeleteMaintenanceRecordCommand: IRequest<Response<string>>
    {
        public int Id { get; set; }
        public class DeleteMaintenanceRecordCommandHandler : IRequestHandler<DeleteMaintenanceRecordCommand, Response<string>>
        {
            private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _Repository;
            private readonly IMapper _mapper;
            public DeleteMaintenanceRecordCommandHandler(IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> Repository, IMapper mapper)
            {
                _Repository = Repository;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteMaintenanceRecordCommand request, CancellationToken cancellationToken)
            {
                var enviroment = _mapper.Map<Maintenance.Domain.Entities.MaintenanceRecord>(request);
                await _Repository.DeleteAsync(enviroment);
                return new Response<string>(enviroment.Id.ToString());
            }
        }
    }
}
