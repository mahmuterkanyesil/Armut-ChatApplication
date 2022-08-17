using ChatApplication.Service.Services.BlockUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlockUserController : ControllerBase
    {
        private readonly IBlockUserService _blockUserService;

        public BlockUserController(IBlockUserService blockUserService)
        {
            _blockUserService = blockUserService;
        }

        [HttpPost("block-user/{nickName}")]

        public async Task<ActionResult> BlockUser(string nickName)
        {
            var response = await this._blockUserService.BlockUser(nickName);
            return Ok(response);
        }

    }
}
