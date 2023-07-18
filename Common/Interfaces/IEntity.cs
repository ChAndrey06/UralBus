namespace Common.Interfaces;

public interface IEntity<TKey>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}