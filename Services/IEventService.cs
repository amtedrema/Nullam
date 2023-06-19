using Nullam.Models;
using Nullam.ViewModels;

namespace Nullam.Services
{
    public interface IEventService
    {
        EventsByDateViewModel GetEventsByDate();
        EventDetailViewModel? GetEventDetails(string id, ParticipantViewModel? participant = null);
        void CreateEvent(Event obj);
        bool DeleteEvent(Guid id);
    }
}
