using LibraryApp.Core.Entities;
using LibraryApp.Core.Handlers.Queries.QueryResults;
using LibraryApp.Core.Interfaces;
using MediatR;

namespace LibraryApp.Core.Handlers.Queries;

public record GetPatronQuery : IRequest<GetPatronQueryResult>;

public class GetPatronQueryHandler : IRequestHandler<GetPatronQuery, GetPatronQueryResult>
{
    private readonly IRepository<Patron> _patrons;
    private readonly IUserService _userService;
    public GetPatronQueryHandler(IRepository<Patron> patrons, IUserService userService)
    {
        _patrons = patrons;
        _userService = userService;
    }

    public async Task<GetPatronQueryResult> Handle(GetPatronQuery request, CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var patron = await _patrons.GetByIdAsync(patronId, cancellationToken) ??
            throw new Exception($"Can't find patron with Id = {patronId}");
        return new GetPatronQueryResult(patron);
    }
}
