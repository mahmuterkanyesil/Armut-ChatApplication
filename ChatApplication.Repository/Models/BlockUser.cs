using ChatApplication.Core.Entities;

namespace ChatApplication.Repository.Models
{
    public partial class BlockUser : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public bool IsDeleted { get; set; }
        public int BlockedUserId { get; set; }

        public virtual User BlockedUser { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
