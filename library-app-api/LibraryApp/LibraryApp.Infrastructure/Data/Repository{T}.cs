using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly LibraryAppContext _dbContext;

    public Repository(LibraryAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual T GetById(int id)
    {
        return _dbContext.Set<T>().Find(id);
    }

    public virtual IEnumerable<T> List()
    {
        return _dbContext.Set<T>().AsEnumerable();
    }

    public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
        return _dbContext.Set<T>()
               .Where(predicate)
               .AsEnumerable();
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        _dbContext.SaveChanges();
    }

    public void Edit(T entity)
    {
        //TODO Do i need to change the state?
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        _dbContext.SaveChanges();
    }
}