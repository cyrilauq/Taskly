using Taskly.Web.Application.State.Interfaces;

namespace Taskly.Web.Application.State
{
    internal class AuthState : IAuthState
    {
        public bool IsAuthenticated { get => UserName != null; }
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }
    }
}
