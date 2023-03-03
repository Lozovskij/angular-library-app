using LibraryApp.Core.Entities;

namespace LibraryApp.Core.Exceptions;

public class InvalidStatusChangeException : Exception
{
	public InvalidStatusChangeException(BookInstanceStatus curr, BookInstanceStatus next)
		: base($"Can't change status from { curr } to { next }")
	{
	}

    public static readonly (BookInstanceStatus curr, BookInstanceStatus next)[] ExceptionStatusPairs = new[] {
        (BookInstanceStatus.Available, BookInstanceStatus.Overdue),
        (BookInstanceStatus.Available, BookInstanceStatus.CheckedOut),
        (BookInstanceStatus.OnHold, BookInstanceStatus.Overdue),
        (BookInstanceStatus.CheckedOut, BookInstanceStatus.OnHold),
        (BookInstanceStatus.Overdue, BookInstanceStatus.OnHold),
        (BookInstanceStatus.Overdue, BookInstanceStatus.CheckedOut)
    };
}
