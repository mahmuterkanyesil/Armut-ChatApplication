using ChatApplication.Repository.Context;

namespace ChatApplication.Repository.Repositories.BlockUser;

public class BlockUserRepository : BaseRepository<Models.BlockUser>, IBlockUserRepository
{
    public BlockUserRepository(BaseContext context) : base(context)
    {
    }
}