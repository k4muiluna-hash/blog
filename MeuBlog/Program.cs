using MeuBlog.Data;
using Microsoft.EntityFrameworkCore;


using var context = new BlogDataContext();

var users = context.Users.AsNoTracking().ToList();

foreach(var user in users)
{
    Console.Clear();
    Console.WriteLine($"{user.Name} - {user.PasswordHash}");
}