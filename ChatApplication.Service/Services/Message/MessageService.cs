using AutoMapper;
using ChatApplication.Repository.Repositories.BlockUser;
using ChatApplication.Repository.UnitOfWork;
using ChatApplication.Service.Services.Account;
using ChatApplication.Service.Services.BlockUser;
using Microsoft.EntityFrameworkCore;

namespace ChatApplication.Service.Services.Message;

public class MessageService: IMessageService
{
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IBlockUserService _blockUserService;

    public MessageService(IAccountService accountService, IUnitOfWork unitOfWork, IMapper mapper, IBlockUserService blockUserService)
    {
        _accountService = accountService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _blockUserService = blockUserService;
    }

    public async Task SendMessage(MessageCreateViewModel messageCreateViewModel)
    {
        var getSessionUserId = await _accountService.GetActiveUserId();
        var getReciever = await _unitOfWork.UserRepository.Where(u => u.NickName == messageCreateViewModel.NickName).FirstOrDefaultAsync();

        if (getReciever != null)
        {
            var isUserBlocked = await this._blockUserService.IsBlockedUser(messageCreateViewModel.NickName);
            if (!isUserBlocked)
            {
                var messageModel = new MessageModel()
                {
                    Message1 = messageCreateViewModel.Message,
                    RecieverId = getReciever.Id,
                    SenderId = getSessionUserId

                };
                var modelToEntity = _mapper.Map<MessageModel, Repository.Models.Message>(messageModel);
                modelToEntity.CreatedAt = DateTime.Now;
                await this._unitOfWork.MessageRepository.AddAsync(modelToEntity);
                await this._unitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception("Belirtilen kullanıcıya mesaj gönderemezsiniz.");
            }
        }
        else
        {
            throw new Exception("Belirtilen kullanıcı bulunamadı");

        }

    }

    public async Task<List<MessageModel>> GetMessages(string nickName)
    {
        var getSessionUserId = await _accountService.GetActiveUserId();
        var getReciever = await _unitOfWork.UserRepository.Where(u => u.NickName == nickName).FirstOrDefaultAsync();

        if (getReciever != null)
        {
            
            var getAllMessages = await this._unitOfWork.MessageRepository
                .Where(x => (x.SenderId == getSessionUserId && x.RecieverId == getReciever.Id) || (x.SenderId == getReciever.Id && x.RecieverId == getSessionUserId))
                .ToListAsync();
            var entityToModel = _mapper.Map<List<Repository.Models.Message>, List<MessageModel>>(getAllMessages);
            return entityToModel;
        }

        throw new Exception();
    }
}