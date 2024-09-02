using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace NeighDay.Server.Data
{
    public class RepositoryBase<T>(ApplicationDbContext context, ILogger<RepositoryBase<T>> logger) : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly ILogger<RepositoryBase<T>> _logger = logger;

        public async Task<IEnumerable<T>> FindAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByOrderBy(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> keySelector)
        {
            return await _context.Set<T>().Where(predicate).OrderBy(keySelector).ToListAsync();
        }


        public async Task<T?> FindById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Add(T entity)
        {
            _context.Set<T>().Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogSaveError(ex);
            }
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogSaveError(ex);
            }
        }

        public async Task Remove(T entity)
        {
            _context.Set<T>().Remove(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogSaveError(ex);
            }
        }

        private void LogSaveError(Exception? exception)
        {
            _logger.LogError(exception, "Error while saving changes to the database");
        }
    }
}
