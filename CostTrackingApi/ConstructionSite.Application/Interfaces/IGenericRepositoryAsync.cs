using System.Linq.Expressions;

namespace ConstructionSite.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, string>> expr);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAllAsync();
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> predicate);
    }
}
