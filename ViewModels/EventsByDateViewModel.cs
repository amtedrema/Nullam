using Nullam.Models;

namespace Nullam.ViewModels
{
    public class EventsByDateViewModel
    {
        public IList<Event> PastEvents { get; set; } = new List<Event>();
        public IList<Event> FutureEvents { get; set; } = new List<Event>();
    }
}
