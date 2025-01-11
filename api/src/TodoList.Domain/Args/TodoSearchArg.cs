namespace TodoList.Domain.Args
{
    public record TodoSearchArg(Guid? UserId = null, string? Name = null, DateTime? StartTime = null, DateTime? DeleteTime = null, string? Content = null, bool IsDeleted = false);
}
