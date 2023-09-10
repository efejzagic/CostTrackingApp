
using AutoMapper;
using Storage.Application.DTOs.Article;
using Storage.Application.Features.Article.Commands;
using Storage.Application.Features.Article.Queries;
using Storage.Application.Interfaces;
using Storage.Application.Parameters.Article;
using Storage.Domain.Entities;

namespace Storage.Application.Mappings
{
    public class ArticleProfile : Profile
    {
        private readonly ISupplierRepository _supplierRepo;

        public ArticleProfile(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;

            CreateMap<Article, ArticleDTO>()
                  .ForMember(d => d.Supplier, opt => opt.MapFrom(src => _supplierRepo.GetById(src.SupplierId).Result));
            CreateMap<Article, ArticleSupplierDTO>();
            CreateMap<ArticleEditDTO, Article>();
            CreateMap<ArticleCreateDTO, Article>();

            CreateMap<GetAllArticleQuery, ArticleDTO>();
            CreateMap<GetArticleByIdQuery, ArticleDTO>();
            CreateMap<GetArticleByNameQuery, ArticleDTO>();

            CreateMap<DeleteArticleCommand, Article>();


            CreateMap<Article, GetAllArticleParameter>();
            CreateMap<GetAllArticleQuery, GetAllArticleParameter>();

            //CreateMap<CreateArticleDTO, Article>();
        }

    }
}
