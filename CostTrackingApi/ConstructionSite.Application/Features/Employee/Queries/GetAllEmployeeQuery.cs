using AutoMapper;
using ConstructionSite.Application.DTOs.Employee;
using ConstructionSite.Application.Interfaces;
using ConstructionSite.Application.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionSite.Application.Features.Employee.Queries
{
    public class GetAllEmployeeQuery : IRequest<PagedResponse<IEnumerable<EmployeeDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

    }
    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, PagedResponse<IEnumerable<EmployeeDTO>>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Employee> _repository;
        private readonly IMapper _mapper;
        public GetAllEmployeeQueryHandler(IGenericRepositoryAsync<Domain.Entities.Employee> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EmployeeDTO>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<EmployeeDTO>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<EmployeeDTO>>(enviroments);
            return new PagedResponse<IEnumerable<EmployeeDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
