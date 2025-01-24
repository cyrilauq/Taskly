namespace Taskly.Natif.Application.Services.Interface
{
    public interface IStorageService
    {
        Task<bool> SaveAsync<T>(T? value, string? key = null);
        Task<T?> GetAsync<T>(string? key = null);
    }
}
