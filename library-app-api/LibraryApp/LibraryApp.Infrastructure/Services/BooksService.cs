using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;

namespace LibraryApp.Infrastructure.Services;
public class BooksService : IBooksService
{
    private readonly IBookInstancesRepository _bookInstancesRepository;
    private readonly IUserService _userService;
    private readonly LibraryAppContext _context;

    public BooksService(
        IBookInstancesRepository bookInstancesRepository,
        IUserService userService,
        LibraryAppContext context)
    {
        _bookInstancesRepository = bookInstancesRepository;
        _userService = userService;
        _context = context;
    }

    public async Task CancelHold(int bookId)
    {
        var patronId = _userService.GetUserId();

        var bookInstance = _bookInstancesRepository.List(bi => bi.BookId == bookId
            && bi.PatronId == patronId
            && bi.Status == BookInstanceStatus.OnHold).Single();

        bookInstance.Status = BookInstanceStatus.Available;
        bookInstance.PatronId = null;
        _bookInstancesRepository.Edit(bookInstance);
    }

    public async Task Hold(int bookId)
    {
        //TODO unit test it!!!!
        var patronId = _userService.GetUserId();

        var patronHoldsAndOverdue = _context.BookInstances
            .Where(bi =>
                bi.PatronId == patronId &&
                (bi.Status == BookInstanceStatus.Overdue || bi.Status == BookInstanceStatus.OnHold))
            .ToList();

        //User has < then 5 books on hold already
        var holdsCount = patronHoldsAndOverdue.Where(bi => bi.Status == BookInstanceStatus.OnHold).Count();
        if (holdsCount >= 5)//TODO move constants to Core
        {
            throw new Exception();//TODO add message and handle on UI
        }
        //User has < then 2 books overdue
        var overdueCount = patronHoldsAndOverdue.Where(bi => bi.Status == BookInstanceStatus.Overdue).Count();
        if (overdueCount >= 2)
        {
            throw new Exception();//TODO add message and handle on UI
        }

        var availableInstance = _context.BookInstances.FirstOrDefault(
            bi => bi.BookId == bookId &&
            bi.Status == BookInstanceStatus.Available);

        //there is still at least 1 book instance available
        if (availableInstance == null)
        {
            throw new Exception();//TODO add message and handle on UI
        }
        availableInstance.Status = BookInstanceStatus.OnHold;
        availableInstance.PatronId = patronId;
        _bookInstancesRepository.Edit(availableInstance);
    }
}
