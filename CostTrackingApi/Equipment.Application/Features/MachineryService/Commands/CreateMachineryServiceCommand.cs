using Equipment.Application.Interfaces;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Equipment.Application.DTOs.MachineryService;

namespace Equipment.Application.Features.MachineryService.Commands
{
    public partial class CreateMachineryServiceCommand : IRequest<Response<string>>
    {
        public MachineryServiceCreateDTO Value { get; set; }

    }
    public class CreateMachineryServiceCommandHandler : IRequestHandler<CreateMachineryServiceCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> _Repository;
        private readonly IMapper _mapper;
        public CreateMachineryServiceCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.MachineryService> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateMachineryServiceCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Equipment.Domain.Entities.MachineryService>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
