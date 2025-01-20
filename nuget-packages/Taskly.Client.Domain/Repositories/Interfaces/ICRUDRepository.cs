namespace Taskly.Client.Domain.Repositories.Interfaces
{
    public interface ICRUDRepository<T, K>
    {
        Task<T> Create(T entity);
        Task<T> Update(K key, T entity);
        Task<bool> Delete(K key);
        Task<IEnumerable<T>> GetAll();
    }
}
