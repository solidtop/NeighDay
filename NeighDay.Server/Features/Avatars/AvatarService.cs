namespace NeighDay.Server.Features.Avatars
{
    public class AvatarService(IAvatarRepository avatarRepository) : IAvatarService
    {
        private readonly IAvatarRepository _avatarRepository = avatarRepository;

        public async Task<IEnumerable<Avatar>> GetAvatars()
        {
            return await _avatarRepository.FindAll();
        }

        public async Task<Avatar> GetAvatar(int avatarId)
        {
            return await _avatarRepository.FindById(avatarId) ?? throw new AvatarNotFoundException(avatarId);
        }
    }
}
