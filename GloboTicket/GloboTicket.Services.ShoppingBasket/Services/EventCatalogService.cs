using System;
using System.Net.Http;
using System.Threading.Tasks;
using GloboTicket.Services.ShoppingBasket.Entities;
using GloboTicket.Services.ShoppingBasket.Extensions;

namespace GloboTicket.Services.ShoppingBasket.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient _client;
        public EventCatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Event> GetEvent(Guid eventId)
        {
            var response = await _client.GetAsync($"/api/events/{eventId}");
            return await response.ReadContentAs<Event>();
        }
    }
}
