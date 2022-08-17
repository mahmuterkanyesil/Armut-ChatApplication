using ChatApplication.Core.DTOs;

namespace ChatApplication.Service.Services.Account;

public class AccountModel: IBaseModelDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string NickName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ChangedAt { get; set; }
    public bool IsDeleted { get; set; }
}