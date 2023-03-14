namespace TechHub.Core.Entities;

public interface IEntityBase
{
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
