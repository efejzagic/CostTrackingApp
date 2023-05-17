using AutoMapper;
using StorageService.DTO;
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

            CreateMap<Supplier,SupplierDTO>()
                .ForMember(d => d.Articles, opt => opt.MapFrom(src => _articleRepo.GetArticlesBySupplierId(src.Id).Result));

        }
    }
}
