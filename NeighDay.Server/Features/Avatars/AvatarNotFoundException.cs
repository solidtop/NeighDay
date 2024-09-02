using NeighDay.Server.Common.Exceptions;

namespace NeighDay.Server.Features.Avatars
{
    public class AvatarNotFoundException(int avatarId) : NotFoundException($"Avatar with id {avatarId} not found")
    {
    }
}
