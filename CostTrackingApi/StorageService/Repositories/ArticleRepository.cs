using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly StorageDbContext _context;

        public ArticleRepository(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<Article> Create(Article article)
        {
            _context.Article.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await _context.Article.ToListAsync();

        }
    }
}
