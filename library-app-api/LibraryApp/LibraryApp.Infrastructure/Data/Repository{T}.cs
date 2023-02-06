using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Data;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly LibraryAppContext _dbContext;

    public Repository(LibraryAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        // await Context.AddAsync(entity);
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        // In case AsNoTracking is used
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(T entity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>().Where(t => t.Id == id).SingleOrDefaultAsync(cancellationToken); ;
    }

    public virtual Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public virtual async Task<int> CountAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<T>().CountAsync(predicate, cancellationToken);
    }
}