﻿using Microsoft.AspNetCore.Mvc;

using Homies.Models;
using Homies.Services.Interfaces;

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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await eventService.GetEventById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEventViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            await eventService.EditEvent(id, model);
            return RedirectToAction("All", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var userId = GetUserId();

            await eventService.AddJoinEvent(userId, id);
            return RedirectToAction("Joined", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userId = GetUserId();

            await eventService.LeaveEvent(userId, id);
            return RedirectToAction("All", "Event");
        }

        public async Task<IActionResult> Joined()
        {
            var userId = GetUserId();
            var model = await eventService.GetJoinedEvents(userId);
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await eventService.GetDetailsEventViewModel(id);
            return View(model);
        }
    }
}
