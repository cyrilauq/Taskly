namespace Taskly.Client.Application.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Type of the model</typeparam>
    /// <typeparam name="K">Type of the model's key</typeparam>
    public interface ICRUDService<T, K>
    {
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(K entityKey, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(K key, T updatedEntity);
    }
}
