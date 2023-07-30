using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Features.ConstructionSite.Queries;
using ConstructionSite.Application.Parameters.ConstructionSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Mappings
{
    public class ConstructionSiteProfile : Profile
    {
        public ConstructionSiteProfile()
        {
            CreateMap<Domain.Entities.ConstructionSite, ConstructionSiteDTO>();
            CreateMap<GetAllConstructionSiteQuery, ConstructionSiteDTO>();
            CreateMap<GetAllConstructionSiteQuery, GetAllConstructionSiteParameter>();
            CreateMap<GetConstructionByIdQuery, ConstructionSiteDTO>();
            CreateMap<GetConstructionByNameQuery, ConstructionSiteDTO>();
        }

    }
}
