namespace LibraryApp.Core.Interfaces;
public interface IBooksService
{
    Task Hold(int bookId);
    Task CancelHold(int bookId);
}
