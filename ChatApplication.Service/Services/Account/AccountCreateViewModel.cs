using ChatApplication.Core.DTOs;

namespace ChatApplication.Service.Services.Account;

public class AccountCreateViewModel : IBaseViewDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string Email { get; set; } = null!;
}