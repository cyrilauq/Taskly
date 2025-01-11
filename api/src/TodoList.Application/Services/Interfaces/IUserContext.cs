namespace TodoList.Application.Services.Interfaces
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
    }
}
