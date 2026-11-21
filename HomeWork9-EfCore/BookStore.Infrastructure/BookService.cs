using BookStore.Domain.Entities;
using BookStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.DTOs;


namespace BookStore.Application.Services
{
    public class BookService : IBookService
    {
        private readonly BookStoreDbContext _context;

        public BookService(BookStoreDbContext context)
        {
            _context = context;
        }

        // 1. Add a book; if the title already exists, increase its stock.
        public async Task AddAsync(Book book)
        {
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.Title == book.Title);

            if (existingBook is not null)
            {
                existingBook.StockQuantity++;
            }
            else
            {
                _context.Books.Add(book);
            }

            await _context.SaveChangesAsync();
        }

        // 2. Update a book.
        public async Task<bool> UpdateAsync(
            int id,
            UpdateBookRequest request)
        {
            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook is null)
                return false;

            existingBook.Title = request.Title;
            existingBook.Author = request.Author;
            existingBook.Description = request.Description;
            existingBook.ISBN = request.ISBN;
            existingBook.Price = request.Price;
            existingBook.StockQuantity = request.StockQuantity;
            existingBook.UpdatedAtUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }


        // 3. Delete a book by id.
        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book is null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        // 4. Get a book by id.
        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // 5. Get only the titles (projection).
        public async Task<List<string>> GetTitlesAsync()
        {
            return await _context.Books
                .Select(b => b.Title)
                .ToListAsync();
        }

        // 6. Get one page of books.
        public async Task<List<Book>> GetPageAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return await _context.Books
                .OrderBy(b => b.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
