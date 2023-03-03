using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryApp.Core.Handlers.Commands;

public record CancelHoldCommand(int BookId) : IRequest;

public class CancelHoldCommandHandler : IRequestHandler<CancelHoldCommand>
{
    private IBookInstancesRepository _bookInstanceRepository;
    private IUserService _userService;

    public CancelHoldCommandHandler(IBookInstancesRepository bookInstanceRepository, IUserService userService)
    {
        _bookInstanceRepository = bookInstanceRepository;
        _userService = userService;
    }

    public async Task Handle(CancelHoldCommand request, CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var book = await _bookInstanceRepository.GetPatronBookAsync(patronId, request.BookId, cancellationToken);
        book.PatronId = null;
        book.SetStatus(BookInstanceStatus.Available);
        await _bookInstanceRepository.UpdateAsync(book, cancellationToken);
    }
}
