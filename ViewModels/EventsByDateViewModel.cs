using Nullam.Models;

namespace Nullam.ViewModels
{
    /// <summary>
    /// Represents the viewmodel for events grouped by date
    /// </summary>
    public class EventsByDateViewModel
    {
        /// <summary>
        /// Gets or sets the list of past events
        /// </summary>
        public IList<Event> PastEvents { get; set; } = new List<Event>();

        /// <summary>
        /// Gets or sets the list of future events
        /// </summary>
        public IList<Event> FutureEvents { get; set; } = new List<Event>();
    }
}
