using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application
{
    public interface IBookService
    {
        Task AddAsync(Book book);
        Task<bool> UpdateAsync(int id, UpdateBookRequest request);

        Task<bool> DeleteAsync(int id);
        Task<Book?> GetByIdAsync(int id);
        Task<List<string>> GetTitlesAsync();
        Task<List<Book>> GetPageAsync(int pageNumber, int pageSize);
    }
}
