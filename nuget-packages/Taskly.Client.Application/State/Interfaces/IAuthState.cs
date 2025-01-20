namespace Taskly.Client.Application.State.Interfaces
{
    public interface IAuthState
    {
        public delegate void StateChangedHandler();

        public event StateChangedHandler OnStateChange;
        public bool IsAuthenticated { get; }
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }
        public string? Token { get; set; }

        public void NotifyStateChanged();
    }
}
