using NeighDay.Server.Features.Chats.Channels;

namespace NeighDay.Server.Features.Chats
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessageResponse>> GetChatHistory(int channelId);
        Task<ChatMessageResponse> CreateMessage(string userId, ChatMessageRequest request);
        Task<IEnumerable<ChatChannel>> GetChannels();
        Task<ChatChannel> GetChannel(int channelId);
    }
}
