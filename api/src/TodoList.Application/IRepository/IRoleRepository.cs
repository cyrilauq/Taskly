using TodoList.Application.Args;
using TodoList.Application.IRepository.Common;

namespace TodoList.Application.IRepository
{
    public interface IRoleRepository : ISearchableRepository<string, RoleSearchArgs>
    {
    }

    public record RoleSearchArgs(string? UserName = null, int? Id = null, bool? SearchExact = null) : BaseSearchArg(Id, SearchExact);
}
