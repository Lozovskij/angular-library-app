using LibraryApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.Handlers.Queries.QueryResults;

public class GetBookQueryResult
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
    public List<string> Authors { get; set; }
    public GetBookQueryResult(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Description = book.Description;
        ISBN = book.ISBN;
        YearOfPublication = book.YearOfPublication;
        Authors = book.Authors.Select(a => a.Name).ToList();
    }
}