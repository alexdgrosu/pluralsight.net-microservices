using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Services.EventCatalog.DbContexts;
using GloboTicket.Services.EventCatalog.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.EventCatalog.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventCatalogDbContext _dbContext;

        public EventRepository(EventCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Event> GetEventById(Guid eventId)
        {
            return await _dbContext.Events
                .Include(x => x.Category)
                .Where(x => x.EventId == eventId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Event>> GetEvents(Guid categoryId)
        {
            return await _dbContext.Events
                .Include(x => x.Category)
                .Where(x => x.CategoryId == categoryId || categoryId == Guid.Empty)
                .ToArrayAsync();
        }
    }
}
