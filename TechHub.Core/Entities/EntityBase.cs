namespace TechHub.Core.Entities;

public abstract class EntityBase : IEntityBase
{
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
