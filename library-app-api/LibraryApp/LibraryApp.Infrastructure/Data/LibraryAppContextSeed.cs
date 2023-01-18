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

            if (!await dbContext.DemoInfo.AnyAsync())
            {
                await dbContext.DemoInfo.AddRangeAsync(
                    GetPreconfiguredDemoInfo());
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Patrons.AnyAsync())
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

	private static IEnumerable<DemoInfo> GetPreconfiguredDemoInfo()
	{
		return new List<DemoInfo>() { new DemoInfo() { Id = 1 } };
	}

	private static IEnumerable<Patron> GetPreconfiguredPatrons()
	{
        var passwordHashStr = "7YScPE3aW7qKhj/p2EhBhMmksT1xTsh2BChP0IJmPrx10fimbn3bitrTFIhM9boHebagisf7dUWSBx0RJYuSOQ==";
        var passwordSaltStr = "nnw/qsRS3uUR8rZLOv9/iRaeB2TfB3+kXbJ+esKU520tvT5S3bpb6abnylC08ddgn6RWs+kg9mHN0vw/L3ZkNAPiumff8j2NfCXSRiE43lH1niMc8TvrzX0vsJoiBqqtwyHuRcSRm/L+NFv3SNG5SBLbyOe9ckPJaJILHRgeYeE=";
        return new List<Patron>()
		{
			new("Ivan", "Lazouski", "SQ42", 1, passwordHashStr, passwordSaltStr),
		};
	}
}
