using Equipment.Application.DTOs.Machinery;
using Equipment.Application.Interfaces;
using Equipment.Application.Parameters.Machinery;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equipment.Application.Features.Machinery.Queries;
using ResponseInfo.Entities;

namespace Equipment.Application.Features.Machinery.Queries
{
    public class GetMachineryByNameQuery : IRequest<Response<MachineryDTO>>
    {
        public string Name { get; set; }
    }
    public class GetMachineryByNameQueryHandler : IRequestHandler<GetMachineryByNameQuery, Response<MachineryDTO>>
    {
        private readonly IMachineryRepository _repository;
        private readonly IMapper _mapper;
        public GetMachineryByNameQueryHandler(IMachineryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<MachineryDTO>> Handle(GetMachineryByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<MachineryDTO>(request);
            var enviroment = await _repository.GetByName(request.Name);
            if (enviroment == null)
            {
                return new Response<MachineryDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with name = {request.Name}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<MachineryDTO>(enviroment);
            return new Response<MachineryDTO>(enviromentViewModel);
        }
    }
}
