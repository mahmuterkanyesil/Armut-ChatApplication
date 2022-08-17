namespace ChatApplication.Service.Services.Message;

public interface IMessageService
{
    Task SendMessage(MessageCreateViewModel messageCreateViewModel);
    Task<List<MessageModel>> GetMessages(string message);
}   