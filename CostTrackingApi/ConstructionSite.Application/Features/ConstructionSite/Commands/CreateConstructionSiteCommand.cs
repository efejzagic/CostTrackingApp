
using AutoMapper;
using MediatR;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using ResponseInfo.Entities;

namespace ConstructionSite.Application.Features.ConstructionSite.Commands
{
    public partial class CreateConstructionSiteCommand : IRequest<Response<string>>
    {
        public ConstructionSiteCreateDTO Value { get; set; }

    }
    public class CreateConstructionSiteCommandHandler: IRequestHandler<CreateConstructionSiteCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.ConstructionSite> _Repository;
        private readonly IMapper _mapper;
        public CreateConstructionSiteCommandHandler(IGenericRepositoryAsync<Domain.Entities.ConstructionSite> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateConstructionSiteCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Domain.Entities.ConstructionSite>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
