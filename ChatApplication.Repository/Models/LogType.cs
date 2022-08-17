using ChatApplication.Core.Entities;

namespace ChatApplication.Repository.Models
{
    public partial class LogType : IBaseEntity
    {
        public LogType()
        {
            ActivityLogs = new HashSet<ActivityLog>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}
