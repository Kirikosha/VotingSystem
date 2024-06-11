using Microsoft.AspNetCore.Diagnostics;
using System.Web;

namespace UniversityVotingSystem
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = 403;
            httpContext.Response.ContentType = "text/plain";
            await httpContext.Response.WriteAsync($"Exception Thrown. It's text is: {exception.Message} ");
            return true;
        }
    }
}