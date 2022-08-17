using ChatApplication.Core.DTOs;
using ChatApplication.Core.Entities;
using ChatApplication.Core.Service;

namespace ChatApplication.Service.Services.Account;

public interface IAccountService
{
    Task<AccountModel> CreateUser(AccountViewModel entityView);
    Task<string> Login(AccountViewModel accountViewModel);
    Task<int> GetActiveUserId();
}