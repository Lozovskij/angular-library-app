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

    public async Task<IEnumerable<BookInstance>> GetPatronBooksAsync(int patronId, CancellationToken cancellationToken)
    {
        return await GetWhereAsync(bi => bi.PatronId == patronId, cancellationToken);
    }

    public async Task<bool> IsAvailableAsync(int bookId, CancellationToken cancellationToken)
    {
        return (await GetWhereAsync(bi => bi.BookId == bookId, cancellationToken))
            .Any(bi => bi.Status == BookInstanceStatus.Available);
    }

    public async Task<IEnumerable<BookInstance>> GetAvailableBooksAsync(int bookId, CancellationToken cancellationToken)
    {
        return await GetWhereAsync(bi => bi.BookId == bookId && bi.Status == BookInstanceStatus.Available, cancellationToken);
    }

    public async Task<BookInstance> GetPatronBookAsync(int patronId, int bookId, CancellationToken cancellationToken)
    {
        return (await GetWhereAsync(bi => bi.PatronId == patronId && bi.BookId == bookId, cancellationToken))
            .Single();
    }
}
