using Nullam.Models;

namespace Nullam.ViewModels
{
	public class ParticipantViewModel
	{
		public Company Company { get; set; }
		public Person Person { get; set; }
		public bool IsCompany { get; set; }
		public string? EventId { get; set; }

		public ParticipantViewModel()
		{
			Company = new Company() { Name = "" };
			Person = new Person() { FirstName = "" };
		}
	}
}
