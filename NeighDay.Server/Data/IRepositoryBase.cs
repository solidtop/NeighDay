using System.Linq.Expressions;

namespace NeighDay.Server.Data
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> FindAll();
        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindByOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> keySelector);
        Task<T?> FindById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(T entity);
    }
}
