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

    public override async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Book>()
            .Include(b => b.Authors)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Book>()
            .Include(b => b.Authors)
            .SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
}
