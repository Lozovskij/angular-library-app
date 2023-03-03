using LibraryApp.Core.Entities;
using LibraryApp.Core.Exceptions;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryApp.Core.Handlers.Commands;

public record HoldBookCommand(int BookId) : IRequest;

public class HoldBookCommandHandler : IRequestHandler<HoldBookCommand>
{
    private readonly IBookInstancesRepository _bookInstancesRepository;
    private readonly IUserService _userService;
    public HoldBookCommandHandler(IBookInstancesRepository bookInstancesRepository, IUserService userService)
    {
        _bookInstancesRepository = bookInstancesRepository;
        _userService = userService;
    }
    public async Task Handle(HoldBookCommand request, CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var patronBooks = await _bookInstancesRepository.GetPatronBooksAsync(patronId, cancellationToken);
        if (patronBooks.Count() >= 5)
        {
            throw new Exception(ExceptionMessages.TooMuchBooksOnHold);
        }
        if (patronBooks.Where(b => b.Status == BookInstanceStatus.Overdue).Count() >= 2)
        {
            throw new Exception(ExceptionMessages.TooMuchBooksOverdue);
        }
        if ((await _bookInstancesRepository.IsAvailableAsync(request.BookId, cancellationToken)) == false)
        {
            throw new Exception(ExceptionMessages.BookIsNotAvailable);
        }
        var book = (await _bookInstancesRepository
            .GetAvailableBooksAsync(request.BookId, cancellationToken))
            .First();
        book.PatronId = patronId;
        book.SetStatus(BookInstanceStatus.OnHold);
        await _bookInstancesRepository.UpdateAsync(book, cancellationToken);
    }
}
