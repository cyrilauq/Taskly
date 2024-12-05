using TodoList.Application.Args;

namespace TodoList.Application.IRepository.Common
{
    public interface ISearchableRepository<T, S> where S : BaseSearchArg
    {
        Task<IEnumerable<T>> Find(S? args = null, CancellationToken cancellationToken = default);
    }
}
