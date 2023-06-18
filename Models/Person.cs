using Nullam.Validators;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
	public class Person : ParticipantBase
	{
		[Required(ErrorMessage = "Palun sisestage eesnimi")]
		public required string FirstName { get; set; }
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Palun sisestage isikukood")]
		[CorrectIdCode(ErrorMessage = "Isikukood ei vasta Eesti vabariigi seatud standardi järgi")]
		[Range(10000000000, 99999999999, ErrorMessage = "Eesti isikukood sisaldab 11 numbrit")]
		public long? IdCode { get; set; }
		
		[StringLength(1500, ErrorMessage = "Lisainfo väli ei saa sisaldada rohkem kui 1500 tähte")]
		public string? Info { get; set; }
	}
}
