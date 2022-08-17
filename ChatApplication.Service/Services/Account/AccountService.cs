using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ChatApplication.Core.DTOs;
using ChatApplication.Core.Middleware;
using ChatApplication.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatApplication.Service.Services.Account;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private IHttpContextAccessor _httpContextAccessor;

    public AccountService(IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AccountModel> CreateUser(AccountViewModel entityView)
    {
        var isExist = await this._unitOfWork.UserRepository.AnyAsync(u => u.Email == entityView.Email && u.NickName == entityView.NickName);
        if (isExist)
        {
            throw new Exception("Başarısız");
        }
        var viewModelToModel = this._mapper.Map<AccountViewModel, AccountModel>(entityView);
        viewModelToModel.CreatedAt = DateTime.Now;
        viewModelToModel.ChangedAt = DateTime.Now;
        viewModelToModel.Password = await this.ConvertToBase64(entityView.Password);

        var modelToUserEntity = this._mapper.Map<AccountModel, Repository.Models.User>(viewModelToModel);
        var createdUser = await this._unitOfWork.UserRepository.AddAsync(modelToUserEntity);
        await this._unitOfWork.CommitAsync();
        var userEntityToModel = this._mapper.Map<Repository.Models.User, AccountModel>(createdUser);
        return userEntityToModel;

    }

    private async Task<string> ConvertToBase64(string password)
    {
        byte[] buffer = System.Text.Encoding.Unicode.GetBytes(password);
        var base64 = System.Convert.ToBase64String(buffer);
        return base64;
    }

    public async Task<string> Login(AccountViewModel accountViewModel)
    {
        var checkUserExist = await this._unitOfWork.UserRepository
            .AnyAsync(u => u.Email == accountViewModel.Email && !u.IsDeleted);
        if (checkUserExist)
        {
            var convertedPassword = await this.ConvertToBase64(accountViewModel.Password);
            var isAccountValid = await this._unitOfWork.UserRepository
                .AnyAsync(u =>
                    u.Email == accountViewModel.Email && u.Password == convertedPassword && !u.IsDeleted);
            if (!isAccountValid)
            {
                throw new Exception("Girilen bilgiler yanlış.");
            }
            var getUser = this._unitOfWork.UserRepository
                .Where(u =>
                    u.Email == accountViewModel.Email && u.Password == convertedPassword && !u.IsDeleted).SingleOrDefault();
            var token = await GenerateToken(getUser);
            return token;
        }
        throw new Exception("Böyle bir kullanıcı bulunamadı.");
    }


    private async Task<string> GenerateToken(Repository.Models.User user)
    {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your secret key value"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("DisplayName", user.NickName),
            new Claim("UserName", user.NickName),
            new Claim("Email", user.Email)
        };

        //Create Security Token object by giving required parameters    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
    public async Task<int> GetActiveUserId()
    {

        var userId = Int32.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
        return userId;
    }
}