namespace Taskly.Web.Application.State.Interfaces
{
    public interface IAuthState
    {
        public bool IsAuthenticated { get; }
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }
    }
}
