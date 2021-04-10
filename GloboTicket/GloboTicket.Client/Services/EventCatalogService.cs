using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GloboTicket.Web.Models.Api;

namespace GloboTicket.Web.Services
{
    public class EventCatalogService : IEventCatalogService
    {

        private readonly HttpClient _client;

        public EventCatalogService(HttpClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<Event>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEvent(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}