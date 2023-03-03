using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Interfaces.Repositories;
public interface IBookInstancesRepository : IRepository<BookInstance>
{
    Task<IEnumerable<BookInstance>> GetPatronBooksAsync(int patronId, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(int bookId, CancellationToken cancellationToken);
    Task<IEnumerable<BookInstance>> GetAvailableBooksAsync(int bookId, CancellationToken cancellationToken);
    Task<BookInstance> GetPatronBookAsync(int patronId, int bookId, CancellationToken cancellationToken);
}
