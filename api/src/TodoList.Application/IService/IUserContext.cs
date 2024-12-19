namespace TodoList.Application.IService
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        Guid UserId { get; }
    }
}
