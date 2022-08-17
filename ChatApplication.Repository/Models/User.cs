using ChatApplication.Core.Entities;

namespace ChatApplication.Repository.Models
{
    public partial class User : IBaseEntity
    {
        public User()
        {
            BlockUserBlockedUsers = new HashSet<BlockUser>();
            BlockUserUsers = new HashSet<BlockUser>();
            MessageRecievers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string NickName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<BlockUser> BlockUserBlockedUsers { get; set; }
        public virtual ICollection<BlockUser> BlockUserUsers { get; set; }
        public virtual ICollection<Message> MessageRecievers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
    }
}
