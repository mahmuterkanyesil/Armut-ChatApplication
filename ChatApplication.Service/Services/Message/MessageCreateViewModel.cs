using ChatApplication.Core.DTOs;

namespace ChatApplication.Service.Services.Message;

public class MessageCreateViewModel : IBaseViewDto
{
    public string Message { get; set; }
    public string NickName { get; set; }
}