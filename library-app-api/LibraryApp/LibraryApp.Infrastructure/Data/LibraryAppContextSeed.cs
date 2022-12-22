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
        }
		catch (Exception)
		{
			throw;
		}
    }
}
