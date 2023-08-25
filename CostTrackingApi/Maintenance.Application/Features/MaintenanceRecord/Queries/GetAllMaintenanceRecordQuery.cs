using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

using Maintenance.Application.Wrappers;
using Maintenance.Application.DTOs.MaintenanceRecord;
using Maintenance.Application.Interfaces;

namespace Maintenance.Application.Features.MaintenanceRecord.Queries
{
    public class GetAllMaintenanceRecordQuery : IRequest<PagedResponse<IEnumerable<MaintenanceRecordDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllMaintenanceRecordQueryHandler: IRequestHandler<GetAllMaintenanceRecordQuery, PagedResponse<IEnumerable<MaintenanceRecordDTO>>>
    {
        private readonly IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> _repository;
        private readonly IMapper _mapper;
        public GetAllMaintenanceRecordQueryHandler(IGenericRepositoryAsync<Maintenance.Domain.Entities.MaintenanceRecord> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<MaintenanceRecordDTO>>> Handle(GetAllMaintenanceRecordQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MaintenanceRecordDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<MaintenanceRecordDTO>>(enviroments);
            return new PagedResponse<IEnumerable<MaintenanceRecordDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}