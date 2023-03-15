namespace TechHub.Core.Entities.Base;

public interface IEntityBase : IAuditableEntity
{
    public int Id { get; set; }
}
