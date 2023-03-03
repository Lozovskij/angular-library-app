using LibraryApp.Core.Exceptions;

namespace LibraryApp.Core.Entities;
public class BookInstance : BaseDemoEntity
{
    public Book Book { get; set; }
    public int BookId { get; set; }
    public Patron? Patron { get; set; } //Patron is null when status == available
    public int? PatronId { get; set; }
    public BookInstanceStatus Status { get; private set; }
    public void SetStatus(BookInstanceStatus nextStatus)
    {
        if (InvalidStatusChangeException.ExceptionStatusPairs.Contains((Status, nextStatus)))
        {
            throw new InvalidStatusChangeException(Status, nextStatus);
        }

        if (nextStatus == BookInstanceStatus.Available && PatronId != null)
        {
            throw new Exception("Can't make book instance available when patron is associated with it");
        }

        if (nextStatus != BookInstanceStatus.Available && PatronId == null)
        {
            throw new Exception("Can't change instance status when there is no patron associated");
        }

        Status = nextStatus;
    }
}

public enum BookInstanceStatus
{
    Available,
    OnHold,
    CheckedOut,
    Overdue
}