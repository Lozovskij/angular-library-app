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
        modelBuilder.Entity<DemoInfo>().HasData(new List<DemoInfo>() {
            new DemoInfo { Id = 1 },
            new DemoInfo { Id = 2 },
            new DemoInfo { Id = 3 }
        });

        modelBuilder
            .Entity<Book>()
            .HasData(new Book {
                Id = 1,
                Title = "War and Peace, Volume 1",
                Description = "Desctiption Test",
                YearOfPublication = 2014,
                DemoId = 1
            });
        modelBuilder
            .Entity<Book>()
            .HasData(new Book
            {
                Id = 2,
                Title = "Book For Demo #1",
                Description = "Desctiption Test",
                YearOfPublication = 2014,
                DemoId = 2
            });
        modelBuilder
            .Entity<Book>()
            .HasData(new Book
            {
                Id = 3,
                Title = "Book For Demo #2",
                Description = "Desctiption Test",
                YearOfPublication = 2014,
                DemoId = 3
            });

        modelBuilder
            .Entity<Author>()
            .HasData(new Author { Id = 1, Name = "Lev Tolstoy", DemoId = 1 });

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
