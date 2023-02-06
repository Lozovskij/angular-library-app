using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Data.Repositories;
public class BookInstancesRepository : Repository<BookInstance>, IBookInstancesRepository
{
    private readonly LibraryAppContext _dbContext;

    public BookInstancesRepository(LibraryAppContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<IEnumerable<BookInstance>> GetWhereAsync(Expression<Func<BookInstance, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<BookInstance>()
            .Include(bi => bi.Book)
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }
}
