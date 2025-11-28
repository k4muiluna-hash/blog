using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mapping;

public class AuthorMap : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Author");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        
        builder.HasMany(x => x.Books)
            .WithMany(x => x.Authors)
            .UsingEntity<Dictionary<string, object>>(
                "AuthorBook",
                book => book.HasOne<Book>()
                    .WithMany()
                    .HasForeignKey("BookId")
                    .HasConstraintName("FK_AuthorBook_BookId"),
                author => author.HasOne<Author>()
                    .WithMany()
                    .HasForeignKey("AuthorId")
                    .HasConstraintName("FK_AuthorBook_AuthorId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}