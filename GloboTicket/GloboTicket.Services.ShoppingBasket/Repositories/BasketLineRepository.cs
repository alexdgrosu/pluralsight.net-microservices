using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.Services.ShoppingBasket.DbContexts;
using GloboTicket.Services.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.ShoppingBasket.Repositories
{
    public class BasketLineRepository : IBasketLineRepository
    {
        private readonly ShoppingBasketDbContext _dbContext;

        public BasketLineRepository(ShoppingBasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BasketLine> AddOrUpdateBasketLine(Guid basketId, BasketLine basketLine)
        {
            var existing = await _dbContext.BasketLines
                .Include(bl => bl.Event)
                .Where(bl => bl.BasketLineId == basketId && bl.EventId == basketLine.EventId)
                .FirstOrDefaultAsync();

            if (existing is null)
            {
                basketLine.BasketId = basketId;
                _dbContext.BasketLines.Add(basketLine);
                return basketLine;
            }

            existing.TicketAmount += basketLine.TicketAmount;
            return existing;
        }

        public async Task<BasketLine> GetBasketLineById(Guid basketLineId)
        {
            return await _dbContext.BasketLines
                .Include(bl => bl.Event)
                .Where(bl => bl.BasketLineId == basketLineId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BasketLine>> GetBasketLines(Guid basketId)
        {
            return await _dbContext.BasketLines
                .Include(bl => bl.Event)
                .Where(bl => bl.BasketId == basketId)
                .ToListAsync();
        }

        public void RemoveBasketLine(BasketLine basketLine)
        {
            _dbContext.BasketLines.Remove(basketLine);
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void UpdateBasketLine(BasketLine basketLine)
        {
            // TODO
        }
    }
}
