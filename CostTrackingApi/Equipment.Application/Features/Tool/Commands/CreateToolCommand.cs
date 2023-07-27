using Equipment.Application.Interfaces;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Equipment.Application.DTOs.Tool;

namespace Equipment.Application.Features.Tool.Commands
{
    public partial class CreateToolCommand : IRequest<Response<string>>
    {
        public ToolCreateDTO Value { get; set; }

    }
    public class CreateToolCommandHandler : IRequestHandler<CreateToolCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> _Repository;
        private readonly IMapper _mapper;
        public CreateToolCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Tool> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateToolCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Equipment.Domain.Entities.Tool>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
