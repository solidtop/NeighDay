using Microsoft.AspNetCore.Identity;
using NeighDay.Server.Features.Users;
using NeighDay.Server.Utils;
using System.Security.Claims;

namespace NeighDay.Server.Features.Auth
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor contextAccessor,
        ILogger<AuthService> logger
        ) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
        private readonly ILogger<AuthService> _logger = logger;

        public async Task<IdentityResult> RegisterUser(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                DisplayColor = ColorGenerator.GenerateDisplayColor(),
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "player");
                _logger.LogInformation("User {Id} succesfully registered email {Email}", user.Id, user.Email);
            }
            else
            {
                _logger.LogError("User {Id} failed to register email {Email}. {Errors}", user.Id, user.Email, result.Errors);
            }

            return result;
        }

        public async Task<SignInResult> LoginUser(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user is null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Id} succesfully logged in username {Username}", user.Id, user.UserName);
            }
            else
            {
                _logger.LogError("User {Id} failed to login username {Username}", user.Id, user.UserName);
            }

            return result;
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();

            var userId = _contextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UserNotFoundException();

            _logger.LogInformation("User {Id} signed out", userId);
        }

        public async Task<UserDetails> GetCurrentUser()
        {
            var userId = _contextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UserNotFoundException();

            var user = await _userManager.FindByIdAsync(userId) ?? throw new UserNotFoundException(userId);
            var roles = await _userManager.GetRolesAsync(user);

            return UserDetails.Create(user, roles);
        }
    }
}
