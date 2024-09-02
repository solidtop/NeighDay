using Microsoft.AspNetCore.Diagnostics;

namespace NeighDay.Server.Common.Exceptions
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string message, DateTime timestamp) = exception switch
            {
                NotFoundException notFound => (404, notFound.Message, DateTime.Now),
                _ => default
            };

            if (statusCode == default)
            {
                return false;
            }

            httpContext.Response.StatusCode = statusCode;
            var response = new ApiResponse(statusCode, message, timestamp);
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
