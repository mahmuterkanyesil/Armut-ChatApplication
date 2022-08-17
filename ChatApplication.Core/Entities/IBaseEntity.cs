namespace ChatApplication.Core.Entities;

public interface IBaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ChangedAt { get; set; }
    public bool IsDeleted { get; set; }
}