using AutoMapper;
using ChatApplication.Repository.Models;
using ChatApplication.Service.Services.Account;
using ChatApplication.Service.Services.BlockUser;
using ChatApplication.Service.Services.Message;

namespace ChatApplication.Service.Mapping;

public class MapProfile: Profile
{
    public MapProfile()
    {
        CreateMap<AccountViewModel, AccountModel>();
        CreateMap<AccountModel, AccountCreateViewModel>();
        CreateMap<AccountModel, User>().ReverseMap();
        CreateMap<AccountLoginViewModel, AccountViewModel>();
        CreateMap<User, LoggedInUserModel>();


        CreateMap<MessageViewModel, MessageModel>();
        CreateMap<MessageModel, MessageCreateViewModel>();
        CreateMap<MessageModel, Message>().ReverseMap();

        CreateMap<BlockUserCreateModel, BlockUser>();
    }
}