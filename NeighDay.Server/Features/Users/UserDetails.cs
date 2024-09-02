namespace NeighDay.Server.Features.Users
{
    public record UserDetails(
        string Id,
        string? Email,
        string? Username,
        IList<string> Roles,
        string? DisplayColor,
        string? AvatarImageUrl
        )
    {
        public static UserDetails Create(ApplicationUser user, IList<string> roles)
        {
            return new UserDetails(

                user.Id,
                user.Email,
                user.UserName,
                roles,
                user?.DisplayColor,
                user?.Avatar?.ImageUrl
            );
        }
    }
}
