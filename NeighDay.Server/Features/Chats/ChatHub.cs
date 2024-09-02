using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace NeighDay.Server.Features.Chats
{
    [Authorize]
    public class ChatHub(IChatService chatService, ILogger<ChatHub> logger) : Hub
    {
        private readonly IChatService _chatService = chatService;
        private readonly ILogger<ChatHub> _logger = logger;

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task<IEnumerable<ChatMessageResponse>> JoinChannel(int channelId)
        {
            var channel = await _chatService.GetChannel(channelId);
            var messages = await _chatService.GetChatHistory(channelId);

            await Groups.AddToGroupAsync(Context.ConnectionId, channel.Name);

            _logger.LogInformation("User joined {channel} channel", channel.Name);

            return messages;
        }

        public async Task LeaveChannel(int channelId)
        {
            var channel = await _chatService.GetChannel(channelId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, channel.Name);
            _logger.LogInformation("User left {channel} channel", channel.Name);
        }

        public async Task SendMessage(ChatMessageRequest request)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return;
            }

            var channel = await _chatService.GetChannel(request.ChannelId);
            var message = await _chatService.CreateMessage(userId, request);
            await Clients.Group(channel.Name).SendAsync("ReceiveMessage", message);
        }
    }
}
