using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;
using Storage.Application.Interfaces;
using Storage.Application.DTOs.Supplier;
using Storage.Application.Parameters.Supplier;
using CorrelationIdLibrary.Interfaces;
using Microsoft.Extensions.Logging;
using Storage.Application.Features.Article.Commands;

namespace Storage.Application.Features.Supplier.Queries
{
    public class GetAllSupplierQuery : IRequest<PagedResponse<IEnumerable<SupplierDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IKey Key { get; set; }

        //public List<MachineryServiceMDTO> ServicingHistory { get; set; }

    }
    public class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQuery, PagedResponse<IEnumerable<SupplierDTO>>>
    {
        private readonly ICorrelationIdGenerator _correlationIdGenerator;
        private readonly ILogger<GetAllSupplierQueryHandler> _logger;
        private readonly IGenericRepositoryAsync<Domain.Entities.Supplier> _repository;
        private readonly IMapper _mapper;
        public GetAllSupplierQueryHandler(IGenericRepositoryAsync<Domain.Entities.Supplier> repository, IMapper mapper, ICorrelationIdGenerator correlationIdGenerator, ILogger<GetAllSupplierQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _correlationIdGenerator = correlationIdGenerator;
            _logger = logger;
        }

        public async Task<PagedResponse<IEnumerable<SupplierDTO>>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("GetAllSupplierQueryHandler: {correlationId}", _correlationIdGenerator.Get());

            var validFilter = _mapper.Map<GetAllSupplierParameter>(request);
            var enviroments = await _repository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var enviromentsViewModel = _mapper.Map<IEnumerable<SupplierDTO>>(enviroments);
            return new PagedResponse<IEnumerable<SupplierDTO>>(enviromentsViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}