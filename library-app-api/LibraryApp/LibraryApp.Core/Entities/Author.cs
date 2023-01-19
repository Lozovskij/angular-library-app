namespace LibraryApp.Core.Entities;
public class Author : BaseDemoEntity
{
    public string Name { get; set; }
    public ICollection<Book> Books { get; set; }
}
