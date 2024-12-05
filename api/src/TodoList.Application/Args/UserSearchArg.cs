namespace TodoList.Application.Args
{
    public record UserSearchArg(int? Id, bool? SearchExact, string? Firstname, string? Lastname, string? Pseudo) : BaseSearchArg(Id, SearchExact);
}
