namespace Entities;

public class Course : IAuditableEntity
{
    public long Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public long AuthorId { get; set; }
    public Author? Author { get; set; }
}