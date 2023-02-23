using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Core.Interfaces;
using MediatR;

namespace LibraryApp.Core.Handlers.Queries;

public record GetBookStatusQuery(int BookId) : IRequest<BookInstanceStatus?>;

public class GetBookStatusQueryHandler : IRequestHandler<GetBookStatusQuery, BookInstanceStatus?>
{
    private readonly IBookInstancesRepository _bookInstancesRepository;
    private readonly IUserService _userService;

    public GetBookStatusQueryHandler(IBookInstancesRepository bookInstancesRepository, IUserService userService)
    {
        _bookInstancesRepository = bookInstancesRepository;
        _userService = userService;
    }

    public async Task<BookInstanceStatus?> Handle(GetBookStatusQuery request, CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var instances = (await _bookInstancesRepository
            .GetWhereAsync(bi => bi.BookId == request.BookId, cancellationToken))
            .ToList();

        if (instances.Count == 0)
        {
            return null;// TODO (book details) better return 'not found' and handle appropriately on front-end (?)
        }

        //book may have many instances, but only one instance can be assosiated with patron
        var status = instances.SingleOrDefault(i => i.PatronId == patronId)?.Status ??
           BookInstanceStatus.Available;
        return status;
    }
}