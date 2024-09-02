using NeighDay.Server.Features.Users;
using System.Collections;
using System.Collections.Concurrent;

namespace NeighDay.Server.Features.Online
{
    public class OnlineStore : IEnumerable<UserSummary>
    {
        private readonly ConcurrentDictionary<string, UserSummary> _onlineUsers = [];

        public void Add(UserSummary user) => _onlineUsers.TryAdd(user.Id, user);
        public void Remove(string userId) => _onlineUsers.TryRemove(userId, out _);

        public IEnumerator<UserSummary> GetEnumerator()
        {
            return _onlineUsers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
