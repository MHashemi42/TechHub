namespace TechHub.Core.Entities.Base;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}
