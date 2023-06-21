using Nullam.Models;
using Nullam.ViewModels;

namespace Nullam.Services
{
    /// <summary>
    /// Represents a service for interacting with events in the application
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves a view model containing events grouped by date
        /// </summary>
        /// <returns>An instance of the <see cref="EventsByDateViewModel"/> class</returns>
        EventsByDateViewModel GetEventsByDate();

        /// <summary>
        /// Retrieves detailed information about an event
        /// </summary>
        /// <param name="id">The identifier of the event</param>
        /// <param name="participant">An optional participant view model</param>
        /// <returns>An instance of the <see cref="EventDetailViewModel"/> class, or null if the event does not exist</returns>
        EventDetailViewModel? GetEventDetails(string id, ParticipantViewModel? participant = null);

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="obj">The event object to create</param>
        void CreateEvent(Event obj);

        /// <summary>
        /// Deletes an event with the specified identifier
        /// </summary>
        /// <param name="id">The identifier of the event to delete</param>
        /// <returns>True if the event was successfully deleted, otherwise false</returns>
        bool DeleteEvent(Guid id);
    }
}
