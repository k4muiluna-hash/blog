using MeuBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuBlog.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");
        builder.HasKey(x => x.Id);;

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("VARCHAR")
            .HasMaxLength(120);

        builder.Property(x => x.Summary)
            .IsRequired()
            .HasColumnName("Summary")
            .HasColumnType("VARCHAR")
            .HasMaxLength(250);

        builder.Property(x => x.Body)
            .IsRequired()
            .HasColumnName("Body")
            .HasColumnType("VARCHAR")
            .HasMaxLength(600);
        
        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.CreateDate)
            .IsRequired()
            .HasColumnName("CreateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        builder.Property(x => x.LastUpdateDate)
            .HasColumnName("LastUpdateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Posts)
            .HasConstraintName("FK_Post_Category")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Posts)
            .HasConstraintName("FK_Post_Author")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "PostTag",
                tag => tag.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .HasConstraintName("FK_PostTag_TagId")
                    .OnDelete(DeleteBehavior.Cascade),
                post => post.HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("PostId")
                    .HasConstraintName("FK_PostTag_PostId")
            );



        builder.HasIndex(x => x.Slug, "IX_Post_Slug")
            .IsUnique();
    }
}