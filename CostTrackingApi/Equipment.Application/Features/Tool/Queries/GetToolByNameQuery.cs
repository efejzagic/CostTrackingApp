using Equipment.Application.DTOs.Tool;
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
using Equipment.Application.Features.Machinery.Queries;

namespace Equipment.Application.Features.Tool.Queries
{
    public class GetToolByNameQuery : IRequest<Response<ToolDTO>>
    {
        public string Name { get; set; }
    }
    public class GetToolByNameQueryHandler : IRequestHandler<GetToolByNameQuery, Response<ToolDTO>>
    {
        private readonly IToolRepository _repository;
        private readonly IMapper _mapper;
        public GetToolByNameQueryHandler(IToolRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ToolDTO>> Handle(GetToolByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ToolDTO> (request);
            var enviroment = await _repository.GetByName(request.Name);
            if (enviroment == null)
            {
                return new Response<ToolDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with name = {request.Name}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ToolDTO>(enviroment);
            return new Response<ToolDTO>(enviromentViewModel);
        }
    }
}
