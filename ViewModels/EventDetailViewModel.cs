using Nullam.Models;

namespace Nullam.ViewModels
{
    /// <summary>
    /// Represents the view model for event details
    /// </summary>
    public class EventDetailViewModel
    {
        /// <summary>
        /// Gets or sets the event details
        /// </summary>
        public required Event Event { get; set; }

        /// <summary>
        /// Gets or sets the list of participants
        /// </summary>
        public IList<ParticipantSimpleViewModel> Participants { get; set; } = new List<ParticipantSimpleViewModel>();

        /// <summary>
        /// Gets or sets the new participant
        /// </summary>
        public ParticipantViewModel NewParticipant { get; set; } = new ParticipantViewModel();
    }
}
