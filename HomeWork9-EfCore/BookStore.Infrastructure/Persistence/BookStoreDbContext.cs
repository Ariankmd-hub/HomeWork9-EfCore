using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(
            DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        

       
    }
}
