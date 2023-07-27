using AutoMapper;
using Storage.Application.DTOs.Supplier;
using Storage.Application.Features.Article.Commands;
using Storage.Application.Features.Article.Queries;
using Storage.Application.Features.Supplier.Commands;
using Storage.Application.Features.Supplier.Queries;
using Storage.Application.Interfaces;
using Storage.Domain.Entities;

namespace Storage.Application.Mappings
{
    public class SupplierProfile : Profile
    {
        private readonly IArticleRepository _articleRepo;

        public SupplierProfile(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;


            CreateMap<Supplier, SupplierArticleDTO>();
            CreateMap<SupplierCreateDTO, Supplier>();
            CreateMap<SupplierEditDTO, Supplier>();

            CreateMap<Supplier, SupplierDTO>()
                .PreserveReferences()
                .ForMember(d => d.Articles, opt => opt.MapFrom(src => _articleRepo.GetArticlesBySupplierId(src.Id).Result));

            CreateMap<GetAllSupplierQuery, SupplierDTO>();
            CreateMap<GetSupplierByIdQuery, SupplierDTO>();
            CreateMap<GetSupplierByNameQuery, SupplierDTO>();

            CreateMap<DeleteSupplierCommand, Supplier>();
        }
    }
}
