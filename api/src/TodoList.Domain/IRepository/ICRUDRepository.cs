using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepository;

public interface ICRUDRepository<T, S> where T : IBaseEntity<Guid>
{
    Task<T> AddAsync(T entity);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(S searchArgs);
    Task<T> UpdateAsync(string id, T entity);
}
