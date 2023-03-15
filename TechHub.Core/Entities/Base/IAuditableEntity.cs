namespace TechHub.Core.Entities.Base;

public interface IAuditableEntity
{
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
}
