namespace LibraryApp.Core.Entities;
public class BookInstance : BaseDemoEntity
{
    public Book Book { get; set; }
    public int BookId { get; set; }

    //TODO: add Patrons history (patron and dates of check in/ check out)
}
