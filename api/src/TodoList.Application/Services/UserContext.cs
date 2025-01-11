using Microsoft.AspNetCore.Http;
using TodoList.Application.Extensions;
using TodoList.Application.Services.Interfaces;

namespace TodoList.Application.Services
{
    internal class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public bool IsAuthenticated =>
            httpContextAccessor
                .HttpContext?
                .User
                .Identity?
                .IsAuthenticated ??
            throw new ApplicationException("User context is unavailable");

        public Guid UserId => Guid.Parse(httpContextAccessor
            .ConnectedUserId() ?? 
            throw new ApplicationException("User context is unavailable"));

    }
}
