using LibraryApp.Core.Entities;

namespace LibraryApp.Infrastructure.Models;

public class BookInstanceDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public BookInstanceStatus Status { get; set; }
    public BookInstanceDto(BookInstance bookInstance)
	{
		Id = bookInstance.Id;
        Title = bookInstance.Book.Title;
        Status = bookInstance.Status;
	}
}