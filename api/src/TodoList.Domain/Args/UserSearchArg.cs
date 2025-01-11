namespace TodoList.Domain.Args
{
    public record UserSearchArg(int? Id, bool? SearchExact, string? Firstname, string? Lastname, string? Pseudo) : BaseSearchArg(Id, SearchExact);
}
