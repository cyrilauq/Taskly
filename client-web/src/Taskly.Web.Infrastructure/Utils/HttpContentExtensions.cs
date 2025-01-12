using Newtonsoft.Json;

namespace Taskly.Web.Infrastructure.Utils
{
    public static class HttpContentExtensions
    {
        public static async Task<T?> ToJson<T>(this HttpContent content) where T : class
        {
            string? responseContent = await content.ReadAsStringAsync();
            return responseContent == null ? null : JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}
