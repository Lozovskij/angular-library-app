using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.Data.Repositories;
public class PatronsRepository : Repository<Patron>, IPatronsRepository
{
    private readonly LibraryAppContext _dbContext;

    public PatronsRepository(LibraryAppContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override Patron GetById(int id)
    {
        return _dbContext.Set<Patron>().Where(p => p.Id == id)
            .Include(p => p.BookInstances)//book in book instance is not available, is it ok?
            .Single();
    }
}
