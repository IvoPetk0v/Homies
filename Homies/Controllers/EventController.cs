using Homies.Models;
using Homies.Services;
using Homies.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public async Task<IActionResult> All()
        {

            var model = await eventService.GetAllEvents();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = await eventService.GetAddEventViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();
                await eventService.AddEventAsync(model, userId);
              return RedirectToAction("All", "Event");
            }

            return View(model);

        }
    }
}
