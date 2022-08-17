namespace ChatApplication.Service.Services.BlockUser;

public class BlockUserModel
{
    public string Message { get; set; }
    public string BlockedUser { get; set; }
    public bool IsSuccesful { get; set; }
}