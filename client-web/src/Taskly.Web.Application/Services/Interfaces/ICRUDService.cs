namespace Taskly.Web.Application.Services.Interfaces
{
    public interface ICRUDService<T>
    {
        Task<T> CreateAsync(T entity);
    }
}
