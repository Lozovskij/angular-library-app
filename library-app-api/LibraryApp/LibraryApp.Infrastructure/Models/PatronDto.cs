using LibraryApp.Core.Entities;

namespace LibraryApp.Infrastructure.Models;
public class PatronDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CardNumber { get; set; }

    //public List<BookInstance> Holds { get; set; }
    //public List<BookInstance> CheckedOutBooks { get; set; }
    // put password related data into owned type (but what are the benefits?)
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public PatronDto(Patron patron)
    {
        FirstName = patron.FirstName;
        LastName = patron.LastName;
        CardNumber = patron.CardNumber;
        PasswordHash = patron.PasswordHash;
        PasswordSalt = patron.PasswordSalt;
    }

}
