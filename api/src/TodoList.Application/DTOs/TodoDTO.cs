namespace TodoList.Application.DTOs
{
    public record TodoDTO(string Id, string Content, string Name, Guid UserId, bool IsDone, DateTime? CreatedOn, DateTime? DeletedOn);
}
