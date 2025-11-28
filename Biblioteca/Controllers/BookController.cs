using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

[Route("/api/[Controller]")]
[ApiController]
public class BookController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromServices] AppDbContext context)
    {
        var books = context.Books.ToList();
        return Ok(books);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var book = context.Books.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return Ok(book);
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] Book book, [FromServices] AppDbContext context)
    {
        var createBook = book;
        context.Books.Add(createBook);
        context.SaveChanges();

        return Created($"/api/book/{createBook.Id}", createBook);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book book, [FromServices] AppDbContext context)
    {
        var updateBook = context.Books.FirstOrDefault(x => x.Id == id);
        if(updateBook == null)
            return NotFound();

        updateBook.Title = book.Title;
        updateBook.CategoryId = book.CategoryId;
        updateBook.CreatedAt = book.CreatedAt;

        context.Books.Update(updateBook);
        context.SaveChanges();        
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteBook([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var deleteBook = context.Books.FirstOrDefault(x => x.Id == id);
        if(deleteBook == null)
            return NotFound();
        
        context.Books.Remove(deleteBook);
        context.SaveChanges();
        return NoContent();
    }
}