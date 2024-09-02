using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NeighDay.Server.Features.Avatars
{
    [ApiController]
    [Authorize]
    [Route("/api/avatars")]
    public class AvatarController(IAvatarService avatarService) : ControllerBase
    {
        private readonly IAvatarService _avatarService = avatarService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Avatar>>> GetAvatars()
        {
            return Ok(await _avatarService.GetAvatars());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Avatar>> GetAvatar(int id)
        {
            return await _avatarService.GetAvatar(id);
        }
    }
}
