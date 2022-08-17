namespace ChatApplication.Service.Services.BlockUser;

public interface IBlockUserService
{
    Task<BlockUserModel> BlockUser(string nickName);
    Task<bool> IsBlockedUser(string nickName);
}