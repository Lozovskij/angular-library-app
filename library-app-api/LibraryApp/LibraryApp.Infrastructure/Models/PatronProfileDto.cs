using LibraryApp.Core.Entities;

namespace LibraryApp.Infrastructure.Models;

public class PatronProfileDto
{
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    //public string CardNumber { get; set; }
    public PatronDto Patron { get; set; }
    public List<BookInstanceDto> Books { get; set; }
    public PatronProfileDto(Patron patron, List<BookInstance> books)
    {
        Patron = new PatronDto(patron);
        Books = new List<BookInstanceDto>();
        Books.AddRange(books.Select(b => new BookInstanceDto(b)));
    }
}
