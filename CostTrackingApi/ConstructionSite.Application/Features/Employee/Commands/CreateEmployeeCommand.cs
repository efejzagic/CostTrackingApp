
using AutoMapper;
using MediatR;
using ConstructionSite.Application.DTOs.ConstructionSite;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Wrappers;
using System.Threading;
using System.Threading.Tasks;
using ConstructionSite.Application.DTOs.Employee;

namespace ConstructionSite.Application.Features.Employee.Commands
{
    public partial class CreateEmployeeCommand : IRequest<Response<string>>
    {
        public EmployeeCreateDTO Value { get; set; }

    }
    public class CreateEmployeeCommandHandler: IRequestHandler<CreateEmployeeCommand, Response<string>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Employee> _Repository;
        private readonly IMapper _mapper;
        public CreateEmployeeCommandHandler(IGenericRepositoryAsync<Domain.Entities.Employee> Repository, IMapper mapper)
        {
            _Repository = Repository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var enviroment = _mapper.Map<Domain.Entities.Employee>(request.Value);
            await _Repository.AddAsync(enviroment);
            return new Response<string>(enviroment.Id.ToString());
        }
    }
}
