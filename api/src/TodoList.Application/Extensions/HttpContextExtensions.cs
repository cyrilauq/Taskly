using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TodoList.Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? ConnectedUserId(this IHttpContextAccessor httpContext)
        {
            return httpContext?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static IEnumerable<string> ConnectedUserRoles(this IHttpContextAccessor httpContext)
        {
            return httpContext?.HttpContext?.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? [];
        }
    }
}
