using Microsoft.EntityFrameworkCore;
using NeighDay.Server.Data;
using NeighDay.Server.Features.Chats.Channels;

namespace NeighDay.Server.Features.Chats
{
    public class ChatRepository(ApplicationDbContext context, ILogger<RepositoryBase<ChatMessage>> logger) : RepositoryBase<ChatMessage>(context, logger), IChatRepository
    {
        public async Task<IEnumerable<ChatMessage>> FindLatest(int channelId, int count)
        {
            return await _context.Set<ChatMessage>()
                .Where(message => message.ChannelId == channelId)
                .OrderByDescending(message => message.Timestamp)
                .Take(count)
                .Reverse().ToListAsync();
        }

        public async Task<IEnumerable<ChatChannel>> FindChannels()
        {
            return await _context.Set<ChatChannel>().ToListAsync();
        }

        public async Task<ChatChannel?> FindChannel(int channelId)
        {
            return await _context.Set<ChatChannel>().FindAsync(channelId);
        }
    }
}
