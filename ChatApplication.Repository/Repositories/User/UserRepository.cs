using ChatApplication.Repository.Context;

namespace ChatApplication.Repository.Repositories.User;

public class UserRepository : BaseRepository<Models.User>, IUserRepository
{
    public UserRepository(BaseContext context) : base(context)
    {
    }
}