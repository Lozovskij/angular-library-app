using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Interfaces;

public interface ITokenService
{
    string Create(Patron patron);
}
