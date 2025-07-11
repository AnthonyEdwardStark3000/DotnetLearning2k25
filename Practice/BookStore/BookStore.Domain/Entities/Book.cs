namespace BookStore.Domain.Entities;

public class Book
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Author { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int YearPublished { get; set; }
}