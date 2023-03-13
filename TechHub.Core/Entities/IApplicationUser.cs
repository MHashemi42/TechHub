namespace TechHub.Core.Entities;

public interface IApplicationUser : IEntityBase<string>
{
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public string AvatarPath { get; set; }
}