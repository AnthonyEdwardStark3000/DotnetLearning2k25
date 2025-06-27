using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Persistence.Services;

public class InMemoryService : IBookService
{
    private List<Book> _books = new();

    /// <summary>
    /// create a new book and store in the _books variable
    /// </summary>
    /// <returns></returns>
    public Task<Book> CreateBookAsync(Book book)
    {
        book.Id = Guid.NewGuid();
        _books.Add(book);
        return Task.FromResult(book);
    }

    /// <summary>
    /// create a new book and store in the _books variable
    /// </summary>
    /// <returns></returns>
    public Task<bool> DeleteBookAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return Task.FromResult(false);

        _books.Remove(book);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Get all books stored in the _books variable
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return Task.FromResult(_books.AsEnumerable());
    }

    /// <summary>
    /// Get a particular book based on ID of the book
    /// </summary>
    /// <returns></returns>
    public Task<Book?> GetBookByIdAsync(Guid id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    /// <summary>
    /// Update a particular book details based on ID of the book
    /// </summary>
    /// <returns></returns>
    public Task<Book?> UpdateBookAsync(Guid id, Book updatedBook)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return Task.FromResult<Book?>(null);
        book.Author = updatedBook.Author;
        book.Title = updatedBook.Title;
        book.YearPublished = updatedBook.YearPublished;
        return Task.FromResult<Book?>(book);
    }
}