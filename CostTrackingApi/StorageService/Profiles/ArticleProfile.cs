using AutoMapper;
using StorageService.DTO;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Profiles
{
    public class ArticleProfile : Profile
    {
        private readonly ISupplierRepository _supplierRepo;

        public ArticleProfile(ISupplierRepository supplierRepo) 
        {
            _supplierRepo = supplierRepo;

            CreateMap<Article, ArticleDTO>()
                .ForMember(d => d.Supplier, opt => opt.MapFrom(src => _supplierRepo.GetById(src.SupplierId).Result));

        }

    }
}
