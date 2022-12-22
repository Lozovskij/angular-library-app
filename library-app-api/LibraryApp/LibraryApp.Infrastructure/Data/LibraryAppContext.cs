using LibraryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;

public class LibraryAppContext : DbContext
{
    public LibraryAppContext(DbContextOptions<LibraryAppContext> options) : base(options) { }

    public DbSet<Patron> Patrons { get; set; }
}
