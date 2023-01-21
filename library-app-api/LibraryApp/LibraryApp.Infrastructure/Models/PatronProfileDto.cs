using LibraryApp.Core.Entities;

namespace LibraryApp.Infrastructure.Models;

public class PatronProfileDto
{
    //public string FirstName { get; set; }
    //public string LastName { get; set; }
    //public string CardNumber { get; set; }
    public PatronDto patron { get; set; }
    public PatronProfileDto(Patron patron)
    {
        this.patron = new PatronDto(patron);
    }
    //public List<BookInstanceDto> Holds { get; set; }
    //public List<BookInstanceDto> CheckedOutBooks { get; set; }
}
