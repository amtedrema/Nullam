using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
	public class Company : ParticipantBase
	{
		[Required(ErrorMessage = "Palun sisestage ettevõtte nimi")]
		public required string Name { get; set; }
		[Required(ErrorMessage = "Palun sisestage osalejate arv")]
        [Range(1, int.MaxValue, ErrorMessage = "Osalejate arv algab ühest inimesest")]
        public int? ParticipantAmount { get; set; }
		[Required(ErrorMessage = "Palun sisestage registrikood")]
		public double? RegistrationCode { get; set; }

		[StringLength(5000, ErrorMessage = "Lisainfo väli ei saa sisaldada rohkem kui 5000 tähte")]
		public string? Info { get; set; }
	}
}
