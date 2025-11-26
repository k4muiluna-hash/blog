using Microsoft.EntityFrameworkCore;

namespace MeuBlog.Data;

public class BlogDataContext : DbContext
{   

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");
    }
}