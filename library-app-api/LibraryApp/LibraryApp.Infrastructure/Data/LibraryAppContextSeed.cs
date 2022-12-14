using LibraryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.Data;
public class LibraryAppContextSeed
{

    public static async Task SeedAsync(LibraryAppContext dbContext)
	{
		try
		{
            dbContext.Database.Migrate();

			if(!await dbContext.Patrons.AnyAsync())
			{
				await dbContext.Patrons.AddRangeAsync(
					GetPreconfiguredPatrons());
				await dbContext.SaveChangesAsync();
            }
        }
		catch (Exception)
		{
			throw;
		}
    }

	private static IEnumerable<Patron> GetPreconfiguredPatrons()
	{
        var passwordHashStr = "7YScPE3aW7qKhj/p2EhBhMmksT1xTsh2BChP0IJmPrx10fimbn3bitrTFIhM9boHebagisf7dUWSBx0RJYuSOQ==";
        var passwordSaltStr = "nnw/qsRS3uUR8rZLOv9/iRaeB2TfB3+kXbJ+esKU520tvT5S3bpb6abnylC08ddgn6RWs+kg9mHN0vw/L3ZkNAPiumff8j2NfCXSRiE43lH1niMc8TvrzX0vsJoiBqqtwyHuRcSRm/L+NFv3SNG5SBLbyOe9ckPJaJILHRgeYeE=";
        return new List<Patron>()
		{
			new("Ivan", "Lazouski", "01-02-03", null, passwordHashStr, passwordSaltStr),
		};
	}
}
