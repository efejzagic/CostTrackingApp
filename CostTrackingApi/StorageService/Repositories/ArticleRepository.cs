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

        public async Task<Article> GetById(int id)
        {
            return await _context.Article.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Article> Edit (Article article)
        {
            var newArticle = await _context.Article.FindAsync(article.Id);

            if (newArticle == null)
            {
                return null;
            }

            newArticle.Name = article.Name;
            newArticle.Quantity = article.Quantity;
            newArticle.Price = article.Price;
            newArticle.Description= article.Description;
            
            await _context.SaveChangesAsync();

            return newArticle;
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

        public Task<bool> SoftDelete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
