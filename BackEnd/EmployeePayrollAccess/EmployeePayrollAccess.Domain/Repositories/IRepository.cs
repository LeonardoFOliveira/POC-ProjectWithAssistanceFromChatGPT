using System.Linq.Expressions;

namespace EmployeePayrollAccess.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<int> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
