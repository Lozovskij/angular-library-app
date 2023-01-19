using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Data.Repositories;
public class BooksRepository : Repository<Book>, IBooksRepository
{
    private readonly LibraryAppContext _dbContext;

    public BooksRepository(LibraryAppContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override IEnumerable<Book> List()
    {
        return _dbContext.Set<Book>().Include(b => b.Authors);
    }

    public override Book GetById(int id)
    {
        return _dbContext.Set<Book>().Include(b => b.Authors).Single(b => b.Id == id);
    }
}
