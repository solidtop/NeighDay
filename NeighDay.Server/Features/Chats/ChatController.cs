using Microsoft.AspNetCore.Mvc;
using NeighDay.Server.Features.Chats.Channels;

namespace NeighDay.Server.Features.Chats
{
    [ApiController]
    //[Authorize]
    [Route("/api/chat")]
    public class ChatController(IChatService chatService) : ControllerBase
    {
        private readonly IChatService _chatService = chatService;

        [HttpGet("channels")]
        public async Task<ActionResult<IEnumerable<ChatChannel>>> GetChannels()
        {
            return Ok(await _chatService.GetChannels());
        }

        [HttpGet("channels/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ChatChannel>> GetChannel(int id)
        {
            return await _chatService.GetChannel(id);
        }
    }
}
