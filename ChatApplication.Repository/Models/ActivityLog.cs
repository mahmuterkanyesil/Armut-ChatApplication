using ChatApplication.Core.Entities;

namespace ChatApplication.Repository.Models
{
    public partial class ActivityLog : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LogTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual LogType LogType { get; set; } = null!;
    }
}
