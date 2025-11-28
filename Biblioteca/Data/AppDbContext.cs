using Biblioteca.Data.Mapping;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=Biblioteca;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorMap());
        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
    }
}