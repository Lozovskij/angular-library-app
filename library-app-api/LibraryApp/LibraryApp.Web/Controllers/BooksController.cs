using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Web.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public BooksController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    [HttpGet]
    public IEnumerable<BookModel> GetBooks()
    {
        return _booksRepository.List().Select(b => new BookModel
        {
            Id = b.Id,
            Title = b.Title,
            Authors = b.Authors.Select(a => a.Name).ToList(),
            Description = b.Description,
        });
    }

    [HttpGet("{id}")]
    public BookModel GetBook([FromQuery] int id)
    {
        Book book = _booksRepository.GetById(id);
        return new BookModel()
        {
            Id = book.Id,
            Title = book.Title,
            Authors = book.Authors.Select(a => a.Name).ToList(),
            Description = book.Description,
        };
    }
}
