using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(p => p.Title)
            .HasMaxLength(100);
        
        builder.Property(p => p.Description)
            .HasMaxLength(1500);
    }
}