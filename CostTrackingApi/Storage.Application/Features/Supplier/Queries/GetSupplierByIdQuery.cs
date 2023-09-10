using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.Application.DTOs.Article;
using AutoMapper;
using Storage.Application.Interfaces;
using ResponseInfo.Entities;
using Storage.Application.DTOs.Supplier;

namespace Storage.Application.Features.Supplier.Queries
{
    public class GetSupplierByIdQuery : IRequest<Response<SupplierDTO>>
    {
        public int Id { get; set; }
    }
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, Response<SupplierDTO>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Supplier> _repository;
        private readonly IMapper _mapper;
        public GetSupplierByIdQueryHandler(IGenericRepositoryAsync<Domain.Entities.Supplier> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<SupplierDTO>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<SupplierDTO>(request);
            var enviroment = await _repository.GetByIdAsync(request.Id);
            if (enviroment == null)
            {
                return new Response<SupplierDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with id = {request.Id}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<SupplierDTO>(enviroment);
            return new Response<SupplierDTO>(enviromentViewModel);
        }
    }
}
