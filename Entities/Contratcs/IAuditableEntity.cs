namespace Entities;

public interface IAuditableEntity : IEntity
{
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
}