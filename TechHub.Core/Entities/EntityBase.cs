﻿namespace TechHub.Core.Entities;

public abstract class EntityBase : IEntityBase
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
