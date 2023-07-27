using Equipment.Application.DTOs.ToolService;
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

namespace Equipment.Application.Features.ToolService.Queries
{
    public class GetToolServiceByNameQuery : IRequest<Response<ToolServiceDTO>>
    {
        public string Name { get; set; }
    }
    public class GetToolServiceByNameQueryHandler : IRequestHandler<GetToolServiceByNameQuery, Response<ToolServiceDTO>>
    {
        private readonly IToolServicingRepository _repository;
        private readonly IMapper _mapper;
        public GetToolServiceByNameQueryHandler(IToolServicingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ToolServiceDTO>> Handle(GetToolServiceByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ToolServiceDTO>(request);
            var enviroment = await _repository.GetByName(request.Name);
            if (enviroment == null)
            {
                return new Response<ToolServiceDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with name = {request.Name}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ToolServiceDTO>(enviroment);
            return new Response<ToolServiceDTO>(enviromentViewModel);
        }
    }
}
