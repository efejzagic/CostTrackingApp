﻿using Microsoft.AspNetCore.Mvc;
using StorageService.DTO.Article;
using StorageService.Services;

namespace StorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleService _articleService;
        public ArticleController(
            ArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetArticles()
        {
            var articles = await _articleService.GetArticles();
            return articles;
        }

        [HttpGet("{id}")]
        public async Task<ArticleDTO> GetById(int id)
        {
            return await _articleService.GetById(id);
        }

        [HttpGet("Supplier/{supplierId}")]
        public async Task<IEnumerable<ArticleDTO>> GetArticlesBySupplierId([FromRoute]int supplierId)
        {
            var articles = await _articleService.GetArticlesBySupplierId(supplierId);
            return articles;
        }


        [HttpPost("Create")]
        public async Task<ArticleDTO> Create(ArticleCreateDTO article)
        {
            var newArticle = article;
            return await _articleService.Create(article);
        }

        [HttpPut("Edit")]
        public async Task<ArticleDTO> Edit (ArticleEditDTO article)
        {
            return await _articleService.Edit(article);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<bool> Delete([FromRoute]int id)
        {
            return await _articleService.Delete(id);
        }

        [HttpPut("SoftDelete/{id}")]
        public async Task<bool> SoftDelete([FromRoute]int id)
        {
            return await _articleService.SoftDelete(id);
        }

    }
}
