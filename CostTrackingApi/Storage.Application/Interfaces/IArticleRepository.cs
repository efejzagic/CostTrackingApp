

using Storage.Domain.Entities;

namespace Storage.Application.Interfaces
{
    public interface IArticleRepository
    {

        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetById(int id);
        Task<Article> Create(Article article);
        Task<Article> Edit(Article article);
        Task<bool> Delete(int id);

        Task<bool> SoftDelete(int id);
        Task<IEnumerable<Article>> GetArticlesBySupplierId(int supplierId);
        Task<Article> GetByName(string name);
    }
}
