using Microsoft.AspNetCore.Identity;
using NeighDay.Server.Features.Avatars;

namespace NeighDay.Server.Features.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string? DisplayColor { get; set; }
        public int? AvatarId { get; set; }
        public Avatar? Avatar { get; set; }
    }
}
