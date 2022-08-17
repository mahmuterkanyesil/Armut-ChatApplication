using AutoMapper;
using ChatApplication.Repository.UnitOfWork;
using ChatApplication.Service.Services.Account;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services.BlockUser;

public class BlockUserService : IBlockUserService
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlockUserService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _accountService = accountService;
    }

    public async Task<BlockUserModel> BlockUser(string nickName)
    {
        var blockUserModel = new BlockUserModel();
        var getSessionUserId = await _accountService.GetActiveUserId();
        var getReciever = await _unitOfWork.UserRepository.Where(u => u.NickName == nickName).FirstOrDefaultAsync();

        if (getReciever != null)
        {
            var getBlockedUser = await this.IsBlockedUser(nickName);   
            if (getBlockedUser)
            {
                blockUserModel.BlockedUser = nickName;
                blockUserModel.IsSuccesful = false;
                blockUserModel.Message = "Belirtilen kullanıcı daha önceden bloklanmış.";
                return blockUserModel;
            }

            var blockUserCreateModel = new BlockUserCreateModel()
            {
                UserId = getSessionUserId,
                BlockedUserId = getReciever.Id,
                CreatedAt = DateTime.Now
            };
            var viewModelToEntity =
                _mapper.Map<BlockUserCreateModel, Repository.Models.BlockUser>(blockUserCreateModel);
            await this._unitOfWork.BlockUserRepository.AddAsync(viewModelToEntity);
            await _unitOfWork.CommitAsync();

            blockUserModel.BlockedUser = nickName;
            blockUserModel.IsSuccesful = true;
            blockUserModel.Message = "Belirtilen kullanıcı bloklandı";
            return blockUserModel;
        }


        blockUserModel.BlockedUser = nickName;
        blockUserModel.IsSuccesful = false;
        blockUserModel.Message = "Belirtilen kullanıcı bulunamadı";
       

        return blockUserModel;
    }

    public async Task<bool> IsBlockedUser(string nickName)
    {
        var getSessionUserId = await _accountService.GetActiveUserId();
        var getReciever = await _unitOfWork.UserRepository.Where(u => u.NickName == nickName).FirstOrDefaultAsync();
        var getBlockedUser = await this._unitOfWork.BlockUserRepository
            .AnyAsync(x =>
                x.UserId == getSessionUserId && x.BlockedUserId == getReciever.Id);
        return getBlockedUser;
    }
}