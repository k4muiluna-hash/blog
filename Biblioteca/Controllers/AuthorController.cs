using Biblioteca.Data;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

[ApiController]
public class AuthorController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Get([FromServices] AppDbContext context)
    {
        var authors = context.Authors.ToList();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById ([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var author = context.Authors.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (author == null)
            return BadRequest();
        return Ok(author);
    }

    [HttpPost("")]
    public IActionResult CreateAuthor([FromBody] Author author, [FromServices] AppDbContext context)
    {
        var user = author;

        context.Authors.Add(user);
        context.SaveChanges();
        return Created($"/{user.Id}", user);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAuthor([FromRoute] int id,[FromBody] Author author, [FromServices] AppDbContext context)
    {
        var user = context.Authors.FirstOrDefault(x => x.Id == id);
        if(user == null)
            return NotFound();
        
        user.Name = author.Name;
        context.Authors.Update(user);
        context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAuthor([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var user = context.Authors.FirstOrDefault(x => x.Id == id);
        if (user == null)
            return NotFound();
        
        context.Authors.Remove(user);
        context.SaveChanges();
        return NoContent();
    }
}