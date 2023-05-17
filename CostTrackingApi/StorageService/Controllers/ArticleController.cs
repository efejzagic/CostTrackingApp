using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepo;
        public ArticleController(IArticleRepository articleRepo)
        {
            _articleRepo = articleRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _articleRepo.GetArticles();
        }

        [HttpPost]
        public async Task<Article> Create(Article article)
        {
            return await _articleRepo.Create(article);
        }

    }
}
