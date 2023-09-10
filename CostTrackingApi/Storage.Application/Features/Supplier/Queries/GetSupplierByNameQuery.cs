
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseInfo.Entities;
using Storage.Application.Interfaces;
using Storage.Application.DTOs.Supplier;

namespace Storage.Application.Features.Supplier.Queries
{
    public class GetSupplierByNameQuery : IRequest<Response<SupplierDTO>>
    {
        public string Name { get; set; }
    }
    public class GetSupplierByNameQueryHandler : IRequestHandler<GetSupplierByNameQuery, Response<SupplierDTO>>
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;
        public GetSupplierByNameQueryHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<SupplierDTO>> Handle(GetSupplierByNameQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<SupplierDTO>(request);
            var enviroment = await _repository.GetByName(request.Name);
            if (enviroment == null)
            {
                return new Response<SupplierDTO>()
                {
                    Succeeded = false,
                    Message = $"No machine found in db with name = {request.Name}",
                    Data = null,

                };
            }
            var enviromentViewModel = _mapper.Map<SupplierDTO>(enviroment);
            return new Response<SupplierDTO>(enviromentViewModel);
        }
    }
}
