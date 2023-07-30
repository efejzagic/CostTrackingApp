using Equipment.Application.Interfaces;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Equipment.Application.DTOs.ToolService;

namespace Equipment.Application.Features.ToolService.Commands
{
    public partial class CreateToolServiceCommand : IRequest<Response<string>>
    {
        public ToolServiceCreateDTO Value { get; set; }

    }
    public class CreateToolServiceCommandHandler : IRequestHandler<CreateToolServiceCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> _Repository;
        private readonly IMapper _mapper;
        public CreateToolServiceCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.ToolService> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateToolServiceCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Equipment.Domain.Entities.ToolService>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
