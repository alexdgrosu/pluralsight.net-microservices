using System;
using System.Threading.Tasks;
using GloboTicket.Services.ShoppingBasket.DbContexts;
using GloboTicket.Services.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.ShoppingBasket.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ShoppingBasketDbContext _dbContext;
        public EventRepository(ShoppingBasketDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public void AddEvent(Event @event)
        {
            _dbContext.Events.Add(@event);
        }

        public async Task<bool> EventExists(Guid eventId)
        {
            return await _dbContext.Events.AnyAsync(e => e.EventId == eventId);
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
