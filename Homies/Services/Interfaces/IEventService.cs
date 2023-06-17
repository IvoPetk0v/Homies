using Homies.Models;

namespace Homies.Services.Interfaces
{
    public interface IEventService
    {
        public Task<ICollection<AllEventViewModel>> GetAllEvents();
        public Task<AddEventViewModel> GetAddEventViewModel();
        public Task AddEventAsync(AddEventViewModel viewModel, string userId);

        public Task<EditEventViewModel> GetEventById(int id);
        public Task EditEvent(int id, EditEventViewModel model);
    }
}
