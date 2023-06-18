namespace Nullam.ViewModels
{
	public class ParticipantSimpleViewModel
	{
		//Company name or Person Id
		public Guid Id { get; set; }
		//Company name or Person full name
		public string? FullName { get; set; }
		//IdCode or RegistrationCode
		public double? Code { get; set; }
		//For ordering
		public DateTime? CreatedDate { get; set; }
		public bool IsCompany { get; set; }
	}
}
