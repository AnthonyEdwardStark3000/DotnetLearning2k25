using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();

    Task<Book?> GetBookByIdAsync(Guid id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(Guid id, Book updatedBook);
    Task<bool> DeleteBookAsync(Guid id);
}