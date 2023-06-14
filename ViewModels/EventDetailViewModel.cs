﻿using Nullam.Models;

namespace Nullam.ViewModels
{
	public class EventDetailViewModel
	{
		public required Event Event { get; set; }
		public IList<ParticipantViewModel> Participants { get; set; } = new List<ParticipantViewModel>();
	}
}