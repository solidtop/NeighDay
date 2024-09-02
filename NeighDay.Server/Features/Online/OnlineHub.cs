using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace NeighDay.Server.Features.Online
{
    [Authorize]
    public class OnlineHub(IOnlineService onlineService) : Hub
    {
        private readonly IOnlineService _onlineService = onlineService;

        public override async Task OnConnectedAsync()
        {
            var user = await _onlineService.AddUser(Context.User);

            var onlineUsers = _onlineService.GetOnlineUsers();

            await Clients.Caller.SendAsync("UpdateOnlineUsers", onlineUsers);
            await Clients.Others.SendAsync("AddOnlineUser", user);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _onlineService.RemoveUser(Context.User);

            await Clients.Others.SendAsync("RemoveOnlineUser", user.Id);
        }
    }
}
