using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeighDay.Server.Features.Auth;

namespace NeighDay.Server.Features.Users
{
    [ApiController]
    [Authorize]
    [Route("/api/user")]
    public class UserController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpGet]
        public async Task<ActionResult<UserDetails>> GetCurrentUser()
        {
            return await _authService.GetCurrentUser();
        }
    }
}
