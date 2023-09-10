using AutoMapper;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Parameters.ConstructionSite;
using MediatR;
using ResponseInfo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.ConstructionSite.Queries
{
    public class GetConstructionByIdQuery : IRequest<Response<ConstructionSiteDTO>>
    {
        public int Id { get; set; }
    }
    public class GetConstructionByIdQueryHandler : IRequestHandler<GetConstructionByIdQuery, Response<ConstructionSiteDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _repository;
        private readonly IMapper _mapper;
        public GetConstructionByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ConstructionSiteDTO>> Handle(GetConstructionByIdQuery request, CancellationToken cancellationToken)
        {
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<ConstructionSiteDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ConstructionSiteDTO>(enviroment);
            return new Response<ConstructionSiteDTO>(enviromentViewModel);
        }
    }
}
