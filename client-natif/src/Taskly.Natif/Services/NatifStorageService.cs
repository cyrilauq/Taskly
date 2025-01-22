using Newtonsoft.Json;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.Services
{
    internal class NatifStorageService : IStorageService
    {
        public Task<bool> SaveAsync<T>(T value, string? key = null)
        {
            string keyValue = key is null ? nameof(T) : key;
            Preferences.Default.Set(key, JsonConvert.SerializeObject(value));
            return Task.FromResult(true);
        }

        public Task<T?> GetAsync<T>(string? key = null)
        {
            string keyValue = key is null ? nameof(T) : key;
            string stringValue = Preferences.Default.Get<string>(keyValue, "");

            return Task.FromResult(JsonConvert.DeserializeObject<T>(stringValue));
        }
    }
}
