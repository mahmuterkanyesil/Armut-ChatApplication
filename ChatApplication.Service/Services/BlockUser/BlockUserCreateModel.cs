namespace ChatApplication.Service.Services.BlockUser;

public class BlockUserCreateModel
{
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int BlockedUserId { get; set; }
}