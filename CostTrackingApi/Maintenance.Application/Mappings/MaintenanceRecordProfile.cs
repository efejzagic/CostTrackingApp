using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Maintenance.Application.DTOs.MaintenanceRecord;
using Maintenance.Application.Features.MaintenanceRecord.Commands;
using Maintenance.Application.Features.MaintenanceRecord.Queries;
using Maintenance.Application.Parameters.MaintenanceRecord;
using Maintenance.Domain.Entities;

namespace Maintenance.Application.Mappings
{
    public class MaintenanceRecordProfile : Profile
    {
       
        public MaintenanceRecordProfile()
        {
            CreateMap<GetAllMaintenanceRecordQuery, MaintenanceRecordDTO>();
            CreateMap<GetMaintenanceRecordByIdQuery,  MaintenanceRecordDTO>();
            CreateMap<MaintenanceRecordCreateDTO,  MaintenanceRecordDTO>();
            CreateMap<MaintenanceRecordEditDTO,  MaintenanceRecordDTO>();
            CreateMap<DeleteMaintenanceRecordCommand, MaintenanceRecord>();
            CreateMap<MaintenanceRecord, MaintenanceRecordDTO>();
            CreateMap<MaintenanceRecordCreateDTO, MaintenanceRecord>();
            CreateMap<MaintenanceRecordEditDTO, MaintenanceRecord>();


            CreateMap<MaintenanceRecord, GetAllMaintenanceRecordParameter>();
            CreateMap<GetAllMaintenanceRecordQuery, GetAllMaintenanceRecordParameter>();

        }

    }
}
