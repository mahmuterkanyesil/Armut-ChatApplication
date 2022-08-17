using ChatApplication.Repository.Repositories.BlockUser;
using ChatApplication.Repository.Repositories.Message;
using ChatApplication.Repository.Repositories.User;

namespace ChatApplication.Repository.UnitOfWork;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IMessageRepository MessageRepository { get; }
    IBlockUserRepository BlockUserRepository { get; }
    Task CommitAsync();
    void Commit();
}