using LibraryApp.Core.Entities;
using LibraryApp.Infrastructure.Models;

namespace LibraryApp.Web.Models;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
    public List<string> Authors { get; set; }
    public BookDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Description = book.Description;
        ISBN = book.ISBN;
        YearOfPublication = book.YearOfPublication;
        Authors = book.Authors.Select(a => a.Name).ToList();
    }
}
