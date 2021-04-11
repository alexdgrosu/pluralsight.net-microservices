using System;
using System.Threading.Tasks;
using GloboTicket.Services.ShoppingBasket.Entities;

namespace GloboTicket.Services.ShoppingBasket.Repositories
{
    public interface IEventRepository
    {
        void AddEvent(Event @event);
        Task<bool> EventExists(Guid eventId);
        Task<bool> SaveChanges();
    }
}
