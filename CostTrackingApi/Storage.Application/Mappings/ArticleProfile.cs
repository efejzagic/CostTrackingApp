﻿
using AutoMapper;
using Equipment.Application.Features.Machinery.Queries;
using Storage.Application.DTOs.Article;
using Storage.Application.Features.Article.Commands;
using Storage.Application.Interfaces;
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



            //CreateMap<CreateArticleDTO, Article>();
        }

    }
}
