using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.Infrastructure.Persistence;

public class BookStoreDbContextFactory
    : IDesignTimeDbContextFactory<BookStoreDbContext>
{
    public BookStoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BookStoreDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");

        return new BookStoreDbContext(optionsBuilder.Options);
    }
}
