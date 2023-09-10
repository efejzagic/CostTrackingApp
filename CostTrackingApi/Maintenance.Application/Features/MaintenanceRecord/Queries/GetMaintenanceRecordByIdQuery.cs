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

namespace Maintenance.Application.Features.MaintenanceRecord.Queries
{
    public class GetMaintenanceRecordByIdQuery : IRequest<Response<MaintenanceRecordDTO>>
    {
        public int Id { get; set; }
    }
    public class GetMaintenanceRecordByIdQueryHandler : IRequestHandler<GetMaintenanceRecordByIdQuery, Response<MaintenanceRecordDTO>>
    {
        private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _repository;
        private readonly IMapper _mapper;
        public GetMaintenanceRecordByIdQueryHandler (IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<MaintenanceRecordDTO>> Handle(GetMaintenanceRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MaintenanceRecordDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<MaintenanceRecordDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<MaintenanceRecordDTO>(enviroment);
            return new Response<MaintenanceRecordDTO>(enviromentViewModel);
        }
    }
}
