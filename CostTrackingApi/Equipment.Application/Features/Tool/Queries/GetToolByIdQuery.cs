using Equipment.Application.DTOs.Machinery;
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
using Equipment.Application.DTOs.Tool;

namespace Equipment.Application.Features.Tool.Queries
{
    public class GetToolByIdQuery : IRequest<Response<ToolDTO>>
    {
        public int Id { get; set; }
    }
    public class GetToolByIdQueryHandler : IRequestHandler<GetToolByIdQuery, Response<ToolDTO>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> _repository;
        private readonly IMapper _mapper;
        public GetToolByIdQueryHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ToolDTO>> Handle(GetToolByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<ToolDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<ToolDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<ToolDTO>(enviroment);
            return new Response<ToolDTO>(enviromentViewModel);
        }
    }
}
