using Equipment.Application.DTOs.MachineryService;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equipment.Application.Features.MachineryService.Queries
{
    public class GetMachineryServiceByNameQuery : IRequest<Response<MachineryServiceDTO>>
    {
        public string Name { get; set; }
    }
    public class GetMachineryServiceByNameQueryHandler : IRequestHandler<GetMachineryServiceByNameQuery, Response<MachineryServiceDTO>>
    {
        private readonly IMachineryServicingRepository _repository;
        private readonly IMapper _mapper;
        public GetMachineryServiceByNameQueryHandler(IMachineryServicingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<MachineryServiceDTO>> Handle(GetMachineryServiceByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MachineryServiceDTO>(request);
            var enviroment = await _repository.GetByName(request.Name);
            if (enviroment == null)
            {
                return new Response<MachineryServiceDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with name = {request.Name}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<MachineryServiceDTO>(enviroment);
            return new Response<MachineryServiceDTO>(enviromentViewModel);
        }
    }
}
