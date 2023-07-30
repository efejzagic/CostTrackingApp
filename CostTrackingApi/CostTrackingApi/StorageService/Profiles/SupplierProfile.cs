using AutoMapper;
using StorageService.DTO;
using StorageService.DTO.Supplier;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Profiles
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
