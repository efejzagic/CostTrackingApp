using AutoMapper;
using Storage.Application.DTOs.Supplier;
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

        }
    }
}
