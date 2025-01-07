namespace Taskly.Web.Application.State.Interfaces
{
    public interface IAuthState
    {
        public delegate void StateChangedHandler();

        public event StateChangedHandler OnStateChange;
        public bool IsAuthenticated { get; }
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }

        public void NotifyStateChanged();
    }
}
