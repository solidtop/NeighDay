using NeighDay.Server.Data;
using NeighDay.Server.Features.Chats.Channels;

namespace NeighDay.Server.Features.Chats
{
    public interface IChatRepository : IRepositoryBase<ChatMessage>
    {
        Task<IEnumerable<ChatMessage>> FindLatest(int channelId, int count);
        Task<IEnumerable<ChatChannel>> FindChannels();
        Task<ChatChannel?> FindChannel(int channelId);
    }
}
