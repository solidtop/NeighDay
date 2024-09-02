using NeighDay.Server.Data;

namespace NeighDay.Server.Features.Avatars
{
    public class AvatarRepository(ApplicationDbContext context, ILogger<RepositoryBase<Avatar>> logger) : RepositoryBase<Avatar>(context, logger), IAvatarRepository
    {
    }
}
