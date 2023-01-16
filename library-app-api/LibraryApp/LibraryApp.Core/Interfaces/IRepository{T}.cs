using LibraryApp.Core.Entities;
using System.Linq.Expressions;

namespace LibraryApp.Core.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    T GetById(int id);
    IEnumerable<T> List();
    IEnumerable<T> List(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Delete(T entity);
    void Edit(T entity);
}
