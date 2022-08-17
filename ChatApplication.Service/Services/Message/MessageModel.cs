namespace ChatApplication.Service.Services.Message;

public class MessageModel
{
    public string Message1 { get; set; }
    public int RecieverId { get; set; }
    public int SenderId { get; set; }
    public DateTime CreatedAt { get; set; }
}