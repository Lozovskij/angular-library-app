using LibraryApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Data;

public class LibraryAppContext : DbContext
{
    public LibraryAppContext(DbContextOptions<LibraryAppContext> options) : base(options) { }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookInstance> BookInstances { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<DemoInfo> DemoInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DemoInfo>().HasData(new List<DemoInfo>() {
            new DemoInfo { Id = 1 },
            new DemoInfo { Id = 2 },
            new DemoInfo { Id = 3 }
        });

        modelBuilder.Entity<Book>().HasData(new List<Book>() {
            new Book {
                Id = 1,
                Title = "War and Peace, Volume 1",
                Description = "Desctiption Test",
                ISBN = "979-8589744965",
                YearOfPublication = 2021,
                DemoId = 1
            },
            new Book {
                Id = 2,
                Title = "Test 1",
                ISBN = "979-8589744945",
                YearOfPublication = 2023,
                Description = "Desctiption Test",
                DemoId = 2
            },
            new Book {
                Id = 3,
                Title = "Test 2",
                ISBN = "979-8589744955",
                YearOfPublication = 2023,
                Description = "Desctiption Test",
                DemoId = 3
            },
            new Book {
                Id = 4,
                Title = "The Little Prince",
                Description = "Desctiption Test",
                ISBN = " 978-0547978840",
                YearOfPublication = 2013,
                DemoId = 1
            },
            new Book {
                Id = 5,
                Title = "The Count of Monte Cristo",
                Description = "Desctiption Test",
                ISBN = "978-0140449266",
                YearOfPublication = 2003,
                DemoId = 1
            },
            new Book {
                Id = 6,
                Title = "Brave New World",
                Description = "Desctiption Test",
                ISBN = "978-0060850524",
                YearOfPublication = 2006,
                DemoId = 1
            },
        });

        modelBuilder
            .Entity<Author>()
            .HasData(new List<Author>() {
                new Author { Id = 1, Name = "Lev Tolstoy", DemoId = 1 },
                new Author { Id = 2, Name = "Antoine de Saint-Exupéry", DemoId = 1 },
                new Author { Id = 3, Name = "Alexandre Dumas", DemoId = 1 },
                new Author { Id = 4, Name = "Aldous Huxley", DemoId = 1 },
            });

        modelBuilder
            .Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j =>
                j
                .HasData(
                    new { AuthorsId = 1, BooksId = 1 },
                    new { AuthorsId = 2, BooksId = 4 },
                    new { AuthorsId = 3, BooksId = 5 },
                    new { AuthorsId = 4, BooksId = 6 }
                    )
            );
    }
}
