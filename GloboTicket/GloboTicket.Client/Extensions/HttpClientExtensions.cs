using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GloboTicket.Web.Extensions
{
    public static class HttpClientExtensions
    {
        public static JsonSerializerOptions DefaultSerializerOptions { get; } = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient client, string url, T data)
        {
            StringContent content = GetStringContentFrom(data);
            return client.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient client, string url, T data)
        {
            StringContent content = GetStringContentFrom(data);
            return client.PutAsync(url, content);
        }

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Something went wrong with calling the API: {response.ReasonPhrase}");
            }

            var dataJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(dataJson, DefaultSerializerOptions);
        }

        private static StringContent GetStringContentFrom<T>(T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var content = new StringContent(dataJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
