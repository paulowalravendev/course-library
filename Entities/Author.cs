namespace Entities;

public class Author : IAuditableEntity
{
    public long Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MainCategory { get; set; } = null!;
    public DateTimeOffset DateOfBirth { get; set; }
    public ICollection<Course>? Courses { get; set; }
}