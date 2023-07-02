using Microsoft.EntityFrameworkCore;

using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Homies.Services.Interfaces;

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
                Start = DateTime.Parse(e.Start.ToString("dd-MM-yyyy H:mm")),
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

        public async Task<EditEventViewModel> GetEventById(int id)
        {
            var types = await dbContext
                .Types
                .Select(t => new TypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                }).ToArrayAsync();

            return await dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new EditEventViewModel()
                {
                    Name = e.Name,
                    Description = e.Description,
                    Start =e.Start.ToString("dd/MM/yyyy H:mm"),
                    End = e.End.ToString("dd/MM/yyyy H:mm"),
                    TypeId = e.TypeId,
                    Types = types
                }).FirstAsync();
        }

        public async Task EditEvent(int id, EditEventViewModel model)
        {
            var eventForEdit = await dbContext.Events.FindAsync(id);

            if (eventForEdit != null)
            {
                eventForEdit.Name = model.Name;
                eventForEdit.Description = model.Description;
                eventForEdit.Start = DateTime.Parse(model.Start);
                eventForEdit.End =DateTime.Parse( model.End);
                eventForEdit.TypeId = model.TypeId;

                await dbContext.SaveChangesAsync();
            }
        }

        public async Task AddJoinEvent(string userId, int eventId)
        {
            bool isAlreadyJoined =
                dbContext.EventsParticipants.Any(ep => ep.HelperId == userId && ep.EventId == eventId);
            if (isAlreadyJoined)
            {
                return;
            }
            var ep = new EventParticipant()
            {
                EventId = eventId,
                HelperId = userId
            };

            await dbContext.EventsParticipants.AddAsync(ep);
            await dbContext.SaveChangesAsync();
        }

        public async Task LeaveEvent(string userId, int eventId)
        {
            var eventForLeaving = await dbContext.EventsParticipants
                .FirstAsync(ep => ep.EventId == eventId && ep.HelperId == userId);
            dbContext.EventsParticipants.Remove(eventForLeaving);
            await dbContext.SaveChangesAsync();
        }

        public async Task<DetailsEventViewModel> GetDetailsEventViewModel(int id)
        {
            var model = await dbContext.Events.Where(e => e.Id == id).Select(e => new DetailsEventViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Start = e.Start.ToString("dd-MM-yyyy H:mm"),
                End = e.End.ToString("dd-MM-yyyy H:mm"),
                Organiser = e.Organiser.UserName,
                CreatedOn = e.CreatedOn.ToString("dd-MM-yyyy H:mm"),
                Type = e.Type.Name
            }).FirstAsync();
            return model;
        }

        public async Task<ICollection<AllEventViewModel>> GetJoinedEvents(string userId)
        {
            var allJoinedEvents = await dbContext.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new AllEventViewModel()
                {
                    Id = ep.Event.Id,
                    Name = ep.Event.Name,
                    Start = DateTime.Parse(ep.Event.Start.ToString("dd-MM-yyyy H:mm")),
                    Type = ep.Event.Type.Name,
                }).ToArrayAsync();
            return allJoinedEvents;
        }

    }
}
