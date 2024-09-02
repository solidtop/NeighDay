using Microsoft.AspNetCore.Identity;
using NeighDay.Server.Features.Users;
using System.Security.Claims;

namespace NeighDay.Server.Features.Online
{
    public class OnlineService(OnlineStore onlineUsers, UserManager<ApplicationUser> userManager) : IOnlineService
    {
        private readonly OnlineStore _onlineUsers = onlineUsers;
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public IEnumerable<UserSummary> GetOnlineUsers()
        {
            return [.. _onlineUsers];
        }

        public async Task<UserSummary> AddUser(ClaimsPrincipal? principal)
        {
            var user = await GetUserSummaryOrThrow(principal);
            _onlineUsers.Add(user);
            return user;
        }

        public async Task<UserSummary> RemoveUser(ClaimsPrincipal? principal)
        {
            var user = await GetUserSummaryOrThrow(principal);
            _onlineUsers.Remove(user.Id);
            return user;
        }

        private async Task<UserSummary> GetUserSummaryOrThrow(ClaimsPrincipal? principal)
        {
            var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new UserNotFoundException();
            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);

            return UserSummary.Create(user);
        }
    }
}