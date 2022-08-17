using System.Text.Json.Serialization;
using ChatApplication.Core.Entities;

namespace ChatApplication.Repository.Models
{
    public partial class Message : IBaseEntity
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public bool IsDeleted { get; set; }
        public string Message1 { get; set; } = null!;
        [JsonIgnore]
        public virtual User Reciever { get; set; } = null!;
        [JsonIgnore]
        public virtual User Sender { get; set; } = null!;
    }
}
