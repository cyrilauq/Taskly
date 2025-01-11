using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Taskly.Web.Infrastructure.Utils
{
    public static class HttpContentExtensions
    {
        public static async Task<T?> ToJson<T>(this HttpContent content) where T : class
        {
            using Stream? responseContent = await content.ReadAsStreamAsync();
            return responseContent == null ? null : (await JsonSerializer.DeserializeAsync<T>(responseContent))!;
        }
    }
}
