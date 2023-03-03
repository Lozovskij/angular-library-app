using LibraryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;
public class LibraryAppContextSeed
{
    public static async Task SeedAsync(LibraryAppContext dbContext)
	{
		try
		{
			//dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();

            if (!await dbContext.Patrons.AnyAsync())
			{
				await dbContext.Patrons.AddRangeAsync(
					GetPreconfiguredPatrons());
				await dbContext.SaveChangesAsync();
			}

            if (!await dbContext.BookInstances.AnyAsync())
            {
                await dbContext.BookInstances.AddRangeAsync(
                    GetPreconfiguredBookInstances());
                await dbContext.SaveChangesAsync();
            }
        }
		catch (Exception)
		{
			throw;
		}
    }

	private static IEnumerable<BookInstance> GetPreconfiguredBookInstances()
	{
        var patronBooks = new List<BookInstance>()
        {
            new BookInstance { BookId = 1, DemoId = 1, PatronId = 1 },
            new BookInstance { BookId = 4, DemoId = 1, PatronId = 1 },
            new BookInstance { BookId = 5, DemoId = 1, PatronId = 1 },
            new BookInstance { BookId = 6, DemoId = 1, PatronId = 1 },
        };

        patronBooks[0].SetStatus(BookInstanceStatus.OnHold);
        patronBooks[1].SetStatus(BookInstanceStatus.OnHold);
        patronBooks[2].SetStatus(BookInstanceStatus.CheckedOut);
        patronBooks[3].SetStatus(BookInstanceStatus.Overdue);

        var books = new List<BookInstance>()
		{
			new BookInstance { BookId = 1, DemoId = 1 },
			new BookInstance { BookId = 1, DemoId = 1 },
            new BookInstance { BookId = 1, DemoId = 1 },
            new BookInstance { BookId = 1, DemoId = 1 },

            new BookInstance { BookId = 2, DemoId = 1 },
            new BookInstance { BookId = 2, DemoId = 1 },
            new BookInstance { BookId = 2, DemoId = 1 },
            new BookInstance { BookId = 2, DemoId = 1 },
            new BookInstance { BookId = 2, DemoId = 1 },

            new BookInstance { BookId = 3, DemoId = 1 },
            new BookInstance { BookId = 3, DemoId = 1 },
            new BookInstance { BookId = 3, DemoId = 1 },
            new BookInstance { BookId = 3, DemoId = 1 },
            new BookInstance { BookId = 3, DemoId = 1 },

            new BookInstance { BookId = 4, DemoId = 1 },
            new BookInstance { BookId = 4, DemoId = 1 },
            new BookInstance { BookId = 4, DemoId = 1 },
            new BookInstance { BookId = 4, DemoId = 1 },

            new BookInstance { BookId = 5, DemoId = 1 },
            new BookInstance { BookId = 5, DemoId = 1 },
            new BookInstance { BookId = 5, DemoId = 1 },
            new BookInstance { BookId = 5, DemoId = 1 },

            new BookInstance { BookId = 6, DemoId = 1 },
            new BookInstance { BookId = 6, DemoId = 1 },
            new BookInstance { BookId = 6, DemoId = 1 },
            new BookInstance { BookId = 6, DemoId = 1 },
        };
        books.AddRange(patronBooks);
        return books;
    }

	private static IEnumerable<Patron> GetPreconfiguredPatrons()
	{
        var passwordHashStr = "7YScPE3aW7qKhj/p2EhBhMmksT1xTsh2BChP0IJmPrx10fimbn3bitrTFIhM9boHebagisf7dUWSBx0RJYuSOQ==";
        var passwordSaltStr = "nnw/qsRS3uUR8rZLOv9/iRaeB2TfB3+kXbJ+esKU520tvT5S3bpb6abnylC08ddgn6RWs+kg9mHN0vw/L3ZkNAPiumff8j2NfCXSRiE43lH1niMc8TvrzX0vsJoiBqqtwyHuRcSRm/L+NFv3SNG5SBLbyOe9ckPJaJILHRgeYeE=";
        return new List<Patron>()
		{
			new("Ivan", "Lazouski", "SQ42", 1, passwordHashStr, passwordSaltStr),
			new("Ivan", "Demo1", "490K", 2, passwordHashStr, passwordSaltStr),
			new("Ivan", "Demo2", "500K", 3, passwordHashStr, passwordSaltStr),
		};
	}
}
