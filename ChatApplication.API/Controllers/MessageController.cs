using AutoMapper;
using ChatApplication.Service.Services.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.API.Controllers
{
    [Route("api/message")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }
        [HttpPost("send-message")]
        public async Task<ActionResult> SendMessage(MessageCreateViewModel messageCreateViewModel)
        {

            await _messageService.SendMessage(messageCreateViewModel);
            return Ok();
        }
        
        [HttpGet("get-messages/{nickName}")]
        public async Task<ActionResult> GetMessages(string nickName)
        {
            var response = await this._messageService.GetMessages(nickName);
            return Ok(response);
        }
    }
}
