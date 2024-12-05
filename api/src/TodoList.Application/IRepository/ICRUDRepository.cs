using TodoList.Domain.Entities;

namespace TodoList.Application.IRepository;

public interface ICRUDRepository<T, S> where T : IBaseEntity<Guid>
{
    Task<T> AddAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(S searchArgs);
    Task<T> UpdateAsync(string id, T entity);
}
