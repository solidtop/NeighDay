using NeighDay.Server.Features.Chats.Channels;
using NeighDay.Server.Features.Users;

namespace NeighDay.Server.Features.Chats
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public string? TextColor { get; set; }
        public DateTime Timestamp { get; set; }
        public int ChannelId { get; set; }
        public required ChatChannel Channel { get; set; }
        public required string UserId { get; set; }
        public required ApplicationUser User { get; set; }

        public override string? ToString()
        {
            return $"{Timestamp.ToShortTimeString()}, {Timestamp.ToShortDateString()}, {Channel?.Name} chat, text: {Text}, author: {User?.UserName}";
        }
    }
}


