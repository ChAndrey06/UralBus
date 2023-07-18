using Common.Interfaces;

namespace BLL.Entities;

public abstract class BaseEntity:IEntity<Guid>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; } = null;
}