using Microsoft.AspNetCore.Identity;
using NeighDay.Server.Features.Users;

namespace NeighDay.Server.Features.Auth
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegisterRequest request);
        Task<SignInResult> LoginUser(LoginRequest request);
        Task LogoutUser();
        Task<UserDetails> GetCurrentUser();
    }
}
