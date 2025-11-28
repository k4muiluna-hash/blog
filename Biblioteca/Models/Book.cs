namespace Biblioteca.Models;

public class Book
{
    public int Id { get; set; }    
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }

    public Category Category { get; set; }

    public List<Author> Authors { get; set; } = [];
}