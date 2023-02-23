using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Handlers.Queries.QueryResults;

public class GetPatronQueryResult
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CardNumber { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public GetPatronQueryResult(Patron patron)
    {
        FirstName = patron.FirstName;
        LastName = patron.LastName;
        CardNumber = patron.CardNumber;
        PasswordHash = patron.PasswordHash;
        PasswordSalt = patron.PasswordSalt;
    }
}
