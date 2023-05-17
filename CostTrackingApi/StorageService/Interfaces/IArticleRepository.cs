using StorageService.Models;

namespace StorageService.Interfaces
{
    public interface IArticleRepository
    {

        Task<IEnumerable<Article>> GetArticles();
        
        Task<Article> Create(Article article);

    }
}
