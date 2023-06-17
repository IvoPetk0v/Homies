using Homies.Models;

namespace Homies.Services.Interfaces
{
    public interface IEventService
    {
        public Task<ICollection<AllEventViewModel>> GetAllEvents();
        public Task<AddEventViewModel> GetAddEventViewModel();
        public Task AddEventAsync(AddEventViewModel viewModel ,string userId);
    }
}
