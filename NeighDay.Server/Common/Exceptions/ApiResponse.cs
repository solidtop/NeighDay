namespace NeighDay.Server.Common.Exceptions
{
    public record ApiResponse(int StatusCode, string Message, DateTime Timestamp)
    {
    }
}
