using TodoList.Domain.Entities;

namespace TodoList.Domain.IRepository;

public interface ICRUDRepository<T, S, K> where T : IBaseEntity<K>
{
    Task<T> AddAsync(T entity);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(S searchArgs);
    Task<T> UpdateAsync(string id, T entity);
    Task<T?> GetByIdAsync(K key);
}
