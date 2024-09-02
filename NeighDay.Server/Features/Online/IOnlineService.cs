using NeighDay.Server.Features.Users;
using System.Security.Claims;

namespace NeighDay.Server.Features.Online
{
    public interface IOnlineService
    {
        IEnumerable<UserSummary> GetOnlineUsers();
        Task<UserSummary> AddUser(ClaimsPrincipal? principal);
        Task<UserSummary> RemoveUser(ClaimsPrincipal? principal);
    }
}
