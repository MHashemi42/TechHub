namespace TechHub.Core.Entities.Base;

public abstract class EntityBase : IEntityBase
{
    public int Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ModifiedOnUtc { get; set; }
}
