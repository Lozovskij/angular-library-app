using LibraryApp.Core.Entities;
using System.Net;

namespace LibraryApp.Core.Handlers.Queries.QueryResults;

public class GetPatronBooksQueryResult
{
    public IEnumerable<BookInstanceDTO>? BookInstances { get; set; }
    public GetPatronBooksQueryResult(IEnumerable<BookInstance> BookInstancesCore)
    {
        BookInstances = BookInstancesCore.Select(bi => new BookInstanceDTO(bi.Id, bi.BookId, bi.Book.Title, bi.Status));
    }
}

public record BookInstanceDTO
(
    int Id,
    int BookId,
    string Title,
    BookInstanceStatus Status
);