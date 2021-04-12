using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GloboTicket.Services.ShoppingBasket.Extensions
{
    public static class HttpClientExtensions
    {
        public static JsonSerializerOptions DefaultSerializerOptions { get; } = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Something went wrong with calling the API: {response.ReasonPhrase}");
            }

            var dataJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(dataJson, DefaultSerializerOptions);
        }
    }
}
