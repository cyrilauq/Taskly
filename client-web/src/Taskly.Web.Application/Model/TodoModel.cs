namespace Taskly.Web.Application.Model
{
    public record TodoModel(string Id, string Content, string Name, Guid UserId, bool IsDone, DateTime? CreatedOn, DateTime? DeletedOn);
}
