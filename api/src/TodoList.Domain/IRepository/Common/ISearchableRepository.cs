using TodoList.Domain.Args;

namespace TodoList.Domain.IRepository.Common
{
    public interface ISearchableRepository<T, S> where S : BaseSearchArg
    {
        Task<IEnumerable<T>> Find(S? args = null, CancellationToken cancellationToken = default);
    }
}
