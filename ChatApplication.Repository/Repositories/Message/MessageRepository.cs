using ChatApplication.Repository.Context;

namespace ChatApplication.Repository.Repositories.Message;

public class MessageRepository: BaseRepository<Models.Message>, IMessageRepository
{
    public MessageRepository(BaseContext context) : base(context)
    {
    }
}