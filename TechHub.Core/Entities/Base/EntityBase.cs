namespace TechHub.Core.Entities.Base;

public abstract class EntityBase : IEntityBase
{
    public int Id { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset ModifiedOn { get; set; }
}
