using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using AutoMapper;
using ChatApplication.Repository.Models;
using ChatApplication.Service.Services.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AccountController(IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        
        [HttpPost("create")]
        public async Task<ActionResult<AccountCreateViewModel>> Create(AccountViewModel accountViewModel)
        {
            var createUser = await this._accountService.CreateUser(accountViewModel);
            var modelToCreateViewModel = this._mapper.Map<AccountModel, AccountCreateViewModel>(createUser);
            return Ok(modelToCreateViewModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AccountLoginViewModel accountLoginViewModel)
        {
            var accountLoginViewModelToAccountViewModel = this._mapper.Map<AccountLoginViewModel, AccountViewModel>(accountLoginViewModel);
            var token = await this._accountService.Login(accountLoginViewModelToAccountViewModel);
            return Ok(token);
        }
        [Authorize]
        [HttpGet("get-active-user")]
        public async Task<IActionResult> GetActiveUsers()
        {
            var response = await this._accountService.GetActiveUserId();
            return Ok(response);
        }


    }
}
