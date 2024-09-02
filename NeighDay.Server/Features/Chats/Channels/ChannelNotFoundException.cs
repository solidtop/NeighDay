using NeighDay.Server.Common.Exceptions;

namespace NeighDay.Server.Features.Chats.Channels
{
    public class ChannelNotFoundException(int channelId) : NotFoundException($"Channel with id {channelId} not found")
    {
    }
}
