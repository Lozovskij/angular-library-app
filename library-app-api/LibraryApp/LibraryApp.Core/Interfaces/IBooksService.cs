namespace LibraryApp.Core.Interfaces;
public interface IBooksService
{
    Task Hold(int bookId, CancellationToken cancellationToken);
    Task CancelHold(int bookId, CancellationToken cancellationToken);
}
