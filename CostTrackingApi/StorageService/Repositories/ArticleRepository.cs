using Microsoft.EntityFrameworkCore;
using StorageService.Data;
using StorageService.Interfaces;
using StorageService.Models;

namespace StorageService.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly StorageDbContext _context;
        private readonly ILogger<ArticleRepository> _logger;
        public ArticleRepository(StorageDbContext context, ILogger<ArticleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Article> Create(Article article)
        {
            _context.Article.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<IEnumerable<Article>> GetArticles()
        {
            _logger.LogInformation("Get All Articles Repo started");
            return await _context.Article.ToListAsync();

        }

        public async Task<Article> GetById(int id)
        {
            return await _context.Article.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Article> Edit (Article article)
        {
            var editArticle = await _context.Article.FindAsync(article.Id);

            if (editArticle == null)
            {
                return null;
            }

            editArticle.Name = article.Name;
            editArticle.Quantity = article.Quantity;
            editArticle.Price = article.Price;
            editArticle.Description= article.Description;
            editArticle.retired = article.retired;

            await _context.SaveChangesAsync();

            return editArticle;
        }

        public async Task<bool> Delete(int id)
        {
            var article = await _context.Article.FindAsync(id);

            if (article == null)
            {
                return false;
            }

            _context.Article.Remove(article);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SoftDelete(int id)
        {
            var article = await GetById(id);

            if (article == null)
            {
                return false;
            }

            article.retired = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Article>> GetArticlesBySupplierId(int supplierId)
        {
            return await _context.Article.Where(a => a.SupplierId == supplierId).ToListAsync();
        }
    }
}
