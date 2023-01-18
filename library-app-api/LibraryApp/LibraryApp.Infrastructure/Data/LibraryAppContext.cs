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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Book>()
            .HasData(new Book {
                Id = 1,
                Title = "War and Peace, Volume 1",
                Description = "Desctiption Test",
                YearOfPublication = 2014
            });

        modelBuilder
            .Entity<Author>()
            .HasData(new Author { Id = 1, Name = "Lev Tolstoy" });

        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j =>
                j
                .HasData(new { AuthorsId = 1, BooksId = 1 })
            );
    }
}
