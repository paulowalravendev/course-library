using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(p => p.FirstName)
            .HasMaxLength(50);
        
        builder.Property(p => p.LastName)
            .HasMaxLength(50);
        
        builder.Property(p => p.MainCategory)
            .HasMaxLength(50);
    }
}