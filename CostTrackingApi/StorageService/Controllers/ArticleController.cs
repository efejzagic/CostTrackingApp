using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.DTO.Article;
using StorageService.Interfaces;
using StorageService.Models;
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

        [HttpGet("Id")]
        public async Task<ArticleDTO> GetById(int id)
        {
            return await _articleService.GetById(id);
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

        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id)
        {
            return await _articleService.Delete(id);
        }

        [HttpPut("SoftDelete")]
        public async Task<bool> SoftDelete(int id)
        {
            return await _articleService.SoftDelete(id);
        }

    }
}
