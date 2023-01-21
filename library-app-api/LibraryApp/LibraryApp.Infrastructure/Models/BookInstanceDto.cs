using LibraryApp.Core.Entities;

namespace LibraryApp.Infrastructure.Models;

public class BookInstanceDto
{
    public int Id { get; set; }

	public BookInstanceDto(BookInstance bookInstance)
	{
		Id = bookInstance.Id;
	}
}