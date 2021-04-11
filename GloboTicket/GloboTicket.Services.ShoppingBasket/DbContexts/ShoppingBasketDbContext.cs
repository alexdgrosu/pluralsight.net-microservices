using GloboTicket.Services.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.Services.ShoppingBasket.DbContexts
{
    public class ShoppingBasketDbContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketLine> BasketLines { get; set; }
        public DbSet<Event> Events { get; set; }

        public ShoppingBasketDbContext(DbContextOptions<ShoppingBasketDbContext> options)
            : base(options)
        {
        }
    }
}
