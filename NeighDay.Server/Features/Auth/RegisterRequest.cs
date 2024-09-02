namespace NeighDay.Server.Features.Auth
{
    public record RegisterRequest(string Username, string Email, string Password)
    {
    }
}
