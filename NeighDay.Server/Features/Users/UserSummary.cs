namespace NeighDay.Server.Features.Users
{
    public record UserSummary(
        string Id,
        string? Name,
        string? DisplayColor,
        string? AvatarImageUrl
        )
    {
        public static UserSummary Create(ApplicationUser user)
        {
            return new UserSummary(
                user.Id,
                user.UserName,
                user?.DisplayColor,
                user?.Avatar?.ImageUrl
                );
        }
    }
}
