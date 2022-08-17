using ChatApplication.Repository.Context;
using ChatApplication.Repository.Repositories.BlockUser;
using ChatApplication.Repository.Repositories.Message;
using ChatApplication.Repository.Repositories.User;

namespace ChatApplication.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BaseContext _context;

    private IUserRepository _userRepository;
    private IBlockUserRepository _blockUserRepository;
    private IMessageRepository _messageRepository;


    public UnitOfWork(BaseContext context)
    {
        _context = new BaseContext();
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    public IBlockUserRepository BlockUserRepository => _blockUserRepository ??= new BlockUserRepository(_context);
    public IMessageRepository MessageRepository => _messageRepository ??= new MessageRepository(_context);

    

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}