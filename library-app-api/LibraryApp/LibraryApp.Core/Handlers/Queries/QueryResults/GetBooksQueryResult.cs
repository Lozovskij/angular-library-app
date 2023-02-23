using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Handlers.Queries.QueryResults;

public class GetBooksQueryResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
    public List<string> Authors { get; set; }
    public GetBooksQueryResult(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Description = book.Description;
        ISBN = book.ISBN;
        YearOfPublication = book.YearOfPublication;
        Authors = book.Authors.Select(a => a.Name).ToList();
    }
}
