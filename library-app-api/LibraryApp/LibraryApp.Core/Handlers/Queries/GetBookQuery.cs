using LibraryApp.Core.Entities;
using LibraryApp.Core.Handlers.Queries.QueryResults;
using LibraryApp.Core.Interfaces.Repositories;
using MediatR;

namespace LibraryApp.Core.Handlers.Queries;

public record GetBookQuery(int BookId) : IRequest<GetBookQueryResult?>;

internal class GetBookQueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResult?>
{
    private readonly IBooksRepository _booksRepository;

    public GetBookQueryHandler(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    public async Task<GetBookQueryResult?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        Book? book = await _booksRepository.GetByIdAsync(request.BookId, cancellationToken);
        return book == null
            ? null
            : new GetBookQueryResult(book);
    }
}
