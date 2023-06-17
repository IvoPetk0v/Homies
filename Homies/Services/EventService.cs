using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Homies.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Homies.Services
{
    public class EventService : IEventService
    {
        private HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;

        }
        public async Task<ICollection<AllEventViewModel>> GetAllEvents()
        {
            return await dbContext.Events.Select(e => new AllEventViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Organiser = e.Organiser.UserName,
                Type = e.Type.Name
            }).ToArrayAsync();
        }

        public async Task<AddEventViewModel> GetAddEventViewModel()
        {
            var types = await dbContext.Types.Select(t => new TypeViewModel()
            {
                Id = t.Id,
                Name = t.Name,
            }).ToArrayAsync();
            var model = new AddEventViewModel()
            {
                Types = types
            };
            return model;


        }

        public async Task AddEventAsync(AddEventViewModel model, string userId)
        {
            var newEvent = new Event()
            {
                Name = model.Name,
                Description = model.Description,
                OrganiserId = userId,
                CreatedOn = DateTime.Now,
                Start = model.Start,
                End = model.End,
                TypeId = model.TypeId

            };
           await dbContext.Events.AddAsync(newEvent);
           await dbContext.SaveChangesAsync();
        }
    }
}
