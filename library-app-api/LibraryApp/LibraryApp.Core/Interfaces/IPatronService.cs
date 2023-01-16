using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Interfaces;
public interface IPatronService
{
    Task<Patron> GenerateRandomPatronAsync();
}
