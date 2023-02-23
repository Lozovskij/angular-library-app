using LibraryApp.Core.Handlers.Queries.QueryResults;
using LibraryApp.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryApp.Core.Handlers.Queries;

public record GetBooksQuery : IRequest<IEnumerable<GetBooksQueryResult>>;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<GetBooksQueryResult>>
{
    private readonly IBooksRepository _booksRepository;

    public GetBooksQueryHandler(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<IEnumerable<GetBooksQueryResult>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return (await _booksRepository.GetAllAsync(cancellationToken))
            .Select(book => new GetBooksQueryResult(book));
    }
}