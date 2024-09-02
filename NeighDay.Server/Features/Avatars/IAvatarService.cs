namespace NeighDay.Server.Features.Avatars
{
    public interface IAvatarService
    {
        Task<IEnumerable<Avatar>> GetAvatars();
        Task<Avatar> GetAvatar(int avatarId);
    }
}
