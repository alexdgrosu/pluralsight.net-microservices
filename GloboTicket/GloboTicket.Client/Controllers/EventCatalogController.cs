using System;
using System.Threading.Tasks;
using GloboTicket.Client.Extensions;
using GloboTicket.Web.Models;
using GloboTicket.Web.Models.Api;
using GloboTicket.Web.Models.View;
using GloboTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Client.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService _eventCatalogService;
        private readonly IShoppingBasketService _shoppingBasketService;
        private readonly Settings _settings;

        public EventCatalogController(IEventCatalogService eventCatalogService, IShoppingBasketService shoppingBasketService, Settings settings)
        {
            _eventCatalogService = eventCatalogService;
            _shoppingBasketService = shoppingBasketService;
            _settings = settings;
        }

        public async Task<IActionResult> Index(Guid categoryId)
        {
            var currentBasketId = Request.Cookies.GetCurrentBasketId(_settings);

            var getBasket = _shoppingBasketService.GetBasket(currentBasketId);
            var getCategories = _eventCatalogService.GetCategories();
            var getEvents = categoryId == Guid.Empty
                ? _eventCatalogService.GetAll()
                : _eventCatalogService.GetByCategoryId(categoryId);

            await Task.WhenAll(new Task[] { getCategories, getEvents, getBasket });

            return View(new EventListModel
            {
                Events = getEvents.Result,
                Categories = getCategories.Result,
                SelectedCategory = categoryId,
                NumberOfItems = getBasket.Result is null ? 0 : getBasket.Result.NumberOfItems
            });
        }

        [HttpPost]
        public IActionResult SelectCategory([FromForm] Guid selectedCategory)
        {
            return RedirectToAction("Index", new { categoryId = selectedCategory });
        }

        public async Task<IActionResult> Detail(Guid eventId)
        {
            var @event = await _eventCatalogService.GetEvent(eventId);
            return View(@event);
        }
    }
}
