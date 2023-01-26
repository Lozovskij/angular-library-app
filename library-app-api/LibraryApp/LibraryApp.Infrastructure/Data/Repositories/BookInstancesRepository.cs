using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data.Repositories;
public class BookInstancesRepository : Repository<BookInstance>, IBookInstancesRepository
{
    private readonly LibraryAppContext _dbContext;

    public BookInstancesRepository(LibraryAppContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<BookInstance> GetBookInstancesByPatronId(int patronId)
    {
        return _dbContext.Set<BookInstance>().Where(bi => bi.PatronId == patronId)
            .Include(bi => bi.Book)
            .AsEnumerable();
    }
}
