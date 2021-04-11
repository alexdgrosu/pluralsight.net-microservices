using System;
using System.Threading.Tasks;
using GloboTicket.Services.ShoppingBasket.DbContexts;
using GloboTicket.Services.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.ShoppingBasket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShoppingBasketDbContext _dbContext;
        public BasketRepository(ShoppingBasketDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBasket(Basket basket)
        {
            _dbContext.Baskets.Add(basket);
        }

        public async Task<bool> BasketExists(Guid basketId)
        {
            return await _dbContext.Baskets.AnyAsync(b => b.BasketId == basketId);
        }

        public async Task<Basket> GetBasketById(Guid basketId)
        {
            return await _dbContext.Baskets
                .Include(b => b.BasketLines)
                .FirstOrDefaultAsync(b => b.BasketId == basketId);
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
