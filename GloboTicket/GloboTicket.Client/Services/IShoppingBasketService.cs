using System;
using System.Threading.Tasks;
using GloboTicket.Web.Models.Api;

namespace GloboTicket.Web.Services
{
    public interface IShoppingBasketService
    {
        Task<Basket> GetBasket(Guid basketId);
    }
}
