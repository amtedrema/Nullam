using Nullam.Models;

namespace Nullam.ViewModels
{
	public class EventDetailViewModel
	{
		public required Event Event { get; set; }
		public IList<ParticipantSimpleViewModel> Participants { get; set; } = new List<ParticipantSimpleViewModel>();
		public ParticipantViewModel NewParticipant { get; set; } = new ParticipantViewModel();
	}
}
