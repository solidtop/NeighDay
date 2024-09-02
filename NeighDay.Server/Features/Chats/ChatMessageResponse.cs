using NeighDay.Server.Features.Users;

namespace NeighDay.Server.Features.Chats
{
    public record ChatMessageResponse(int Id, string Text, string? TextColor, DateTime Timestamp, UserSummary User)
    {
        public static ChatMessageResponse Create(ChatMessage message)
        {
            return new ChatMessageResponse
            (
                message.Id,
                message.Text,
                message.TextColor,
                message.Timestamp,
                UserSummary.Create(message.User)
            );
        }
    }
}

