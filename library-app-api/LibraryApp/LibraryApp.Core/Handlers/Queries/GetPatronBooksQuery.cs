using LibraryApp.Core.Entities;
using LibraryApp.Core.Handlers.Queries.QueryResults;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryApp.Core.Handlers.Queries;

public record GetPatronBooksQuery : IRequest<GetPatronBooksQueryResult>;

public class GetPatronBooksQueryHandler : IRequestHandler<GetPatronBooksQuery, GetPatronBooksQueryResult>
{
    private readonly IBookInstancesRepository _bookInstancesRepository;
    private readonly IUserService _userService;

    public GetPatronBooksQueryHandler(IUserService userService, IBookInstancesRepository bookInstancesRepository)
    {
        _userService = userService;
        _bookInstancesRepository = bookInstancesRepository;
    }

    public async Task<GetPatronBooksQueryResult> Handle(GetPatronBooksQuery request, CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var bookInstances = await _bookInstancesRepository
            .GetWhereAsync(bi => bi.PatronId == patronId, cancellationToken);

        return new GetPatronBooksQueryResult(bookInstances);
    }
}
