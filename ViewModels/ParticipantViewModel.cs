using Nullam.Validators;
using System.ComponentModel.DataAnnotations;

namespace Nullam.ViewModels
{
	public class ParticipantViewModel
	{
		public Guid Id { get; set; }
		public string? Info { get; set; }
		public int PaymentMethodTypeId { get; set; }
		public bool IsCompany { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? EventId { get; set; }
		
		//Company name or Person full name
		public string? FullName { get; set; }

		// IdCode or RegistrationCode
		public double? Code { get; set; }

		//Company properties
		[Required(ErrorMessage = "Palun sisestage ettevõtte nimi")]
		public string? Name { get; set; }
		public int? ParticipantAmount { get; set; }
		[Required(ErrorMessage = "Palun sisestage registrikood")]
		public double? RegistrationCode { get; set; }

		//Person properties
		[Required(ErrorMessage = "Palun sisestage eesnimi")]
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Palun sisestage isikukood")]
		[CorrectIdCode(ErrorMessage = "Ei ole õige isikukood")]
		public double? IdCode { get; set; }
	}
}
