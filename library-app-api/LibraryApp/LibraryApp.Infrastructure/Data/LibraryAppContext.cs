using LibraryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;

public class LibraryAppContext : DbContext
{
    public LibraryAppContext(DbContextOptions<LibraryAppContext> options) : base(options) { }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<DemoInfo> DemoInfo { get; set; }
}
