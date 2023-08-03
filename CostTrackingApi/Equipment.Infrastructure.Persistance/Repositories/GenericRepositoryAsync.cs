using Equipment.Application.Interfaces;
using Equipment.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;

namespace Equipment.Infrastructure.Persistance.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly EquipmentDbContext _dbContext;

        public GenericRepositoryAsync(EquipmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var content = await _dbContext.Set<T>().FindAsync(id);
            if(content!=null)
                _dbContext.Entry(content).State = EntityState.Detached;
            return content;
        }




        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetPagedReponseByIdWithTemporalAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize, Expression<Func<T, string>> expr)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(expr)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }
        public async Task SaveAllAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
