using TodoList.Domain.Args;
using TodoList.Domain.IRepository.Common;

namespace TodoList.Domain.IRepository
{
    public interface IRoleRepository : ISearchableRepository<string, RoleSearchArgs>
    {
    }

    public record RoleSearchArgs(string? UserName = null, int? Id = null, bool? SearchExact = null) : BaseSearchArg(Id, SearchExact);
}
