using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Interfaces;
public interface IRandomPatronGenerator
{
    Task<Patron> GenerateRandomPatronAsync();
}
