using Equipment.Application.Interfaces;
using Equipment.Application.Wrappers;
using AutoMapper;
using Equipment.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Equipment.Application.Features.Machinery.Commands
{
    public partial class CreateMachineryCommand : IRequest<Response<string>>
    {
        public string InternalEnviroment { get; set; }

    }
    public class CreateMachineryCommandHandler : IRequestHandler<CreateMachineryCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> _Repository;
        private readonly IMapper _mapper;
        public CreateMachineryCommandHandler(IGenericRepositoryAsync<Equipment.Domain.Entities.Machinery> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateMachineryCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Equipment.Domain.Entities.Machinery> (request);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
