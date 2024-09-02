using NeighDay.Server.Common.Exceptions;

namespace NeighDay.Server.Features.Users
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User not found")
        {
        }

        public UserNotFoundException(string userId) : base($"User with id {userId} not found")
        {
        }
    }
}
