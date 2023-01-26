namespace LibraryApp.Core.Entities;
public class BookInstance : BaseDemoEntity
{
    public Book Book { get; set; }
    public int BookId { get; set; }
    public Patron? Patron { get; set; } //Patron is null when status == available
    public int? PatronId { get; set; }
    public BookInstanceStatus Status { get; set; }
}

public enum BookInstanceStatus
{
    Available,
    OnHold,
    CheckedOut,
    Overdue
}