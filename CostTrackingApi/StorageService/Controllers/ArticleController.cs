using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.DTO;
using StorageService.Interfaces;
using StorageService.Models;
using StorageService.Services;

namespace StorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepo;
        private readonly ArticleService _articleService;
        public ArticleController(IArticleRepository articleRepo,
            ArticleService articleService)
        {
            _articleRepo = articleRepo;
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetArticles()
        {
            var articles = await _articleService.GetArticles();
            return articles;
        }

        [HttpGet("Id")]
        public async Task<Article> GetById(int id)
        {
            return await _articleRepo.GetById(id);  
        }

        [HttpPost]
        public async Task<Article> Create(Article article)
        {
            return await _articleRepo.Create(article);
        }

        [HttpPut("Edit")]
        public async Task<Article> Edit (Article article)
        {
            return await _articleRepo.Edit(article);
        }

        [HttpDelete("Delete")]
        public async Task<bool> Delete(int id)
        {
            return await _articleRepo.Delete(id);
        }

        [HttpPut("SoftDelete")]
        public async Task<bool> SoftDelete(int id)
        {
            return await _articleRepo.SoftDelete(id);
        }

    }
}
