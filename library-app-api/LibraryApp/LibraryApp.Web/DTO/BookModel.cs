using LibraryApp.Core.Entities;

namespace LibraryApp.Web.DTO;

public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> Authors { get; set; }
}
