using System;
using System.Net.Http;
using System.Threading.Tasks;
using GloboTicket.Web.Extensions;
using GloboTicket.Web.Models;
using GloboTicket.Web.Models.Api;

namespace GloboTicket.Web.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly HttpClient _client;
        private readonly Settings _settings;

        public ShoppingBasketService(HttpClient client, Settings settings)
        {
            _client = client;
            _settings = settings;
        }
        public async Task<Basket> GetBasket(Guid basketId)
        {
            if (basketId == Guid.Empty)
                return default;
            
            var response = await _client.GetAsync($"/api/baskets/{basketId}");
            return await response.ReadContentAs<Basket>();
        }
    }
}
