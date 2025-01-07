using Taskly.Web.Application.State.Interfaces;
using static Taskly.Web.Application.State.Interfaces.IAuthState;

namespace Taskly.Web.Application.State
{
    internal class AuthState : IAuthState
    {
        public bool IsAuthenticated { get => UserName != null; }
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }

        public event StateChangedHandler OnStateChange;

        public void NotifyStateChanged()
        {
            OnStateChange?.Invoke();
        }
    }
}
