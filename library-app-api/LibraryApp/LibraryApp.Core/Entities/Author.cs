namespace LibraryApp.Core.Entities;
public class Author : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}
