using System;
using System.Threading.Tasks;
using GloboTicket.Web.Models.View;
using GloboTicket.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.Client.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService _service;

        public EventCatalogController(IEventCatalogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(Guid categoryId)
        {
            var getCategories = _service.GetCategories();
            var getEvents = categoryId == Guid.Empty
                ? _service.GetAll()
                : _service.GetByCategoryId(categoryId);

            await Task.WhenAll(new Task[] { getCategories, getEvents });

            return View(new EventListModel
            {
                Events = getEvents.Result,
                Categories = getCategories.Result,
                SelectedCategory = categoryId
            });
        }

        [HttpPost]
        public IActionResult SelectCategory([FromForm] Guid selectedCategory)
        {
            return RedirectToAction("Index", new { categoryId = selectedCategory });
        }
    }
}
