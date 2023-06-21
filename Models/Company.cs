using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
    /// <summary>
    /// Represents a participant that is a company
    /// </summary>
    public class Company : ParticipantBase
	{
        /// <summary>
        /// Gets or sets the name of the company
        /// </summary>
        [Required(ErrorMessage = "Palun sisestage ettevõtte nimi")]
		public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the number of participants from the company
        /// </summary>
		[Required(ErrorMessage = "Palun sisestage osalejate arv")]
        [Range(1, int.MaxValue, ErrorMessage = "Osalejate arv algab ühest inimesest")]
        public int? ParticipantAmount { get; set; }

        /// <summary>
        /// Gets or sets the registration code of the company
        /// </summary>
		[Required(ErrorMessage = "Palun sisestage registrikood")]
		public double? RegistrationCode { get; set; }

        /// <summary>
        /// Gets or sets additional information about the company
        /// </summary>
		[StringLength(5000, ErrorMessage = "Lisainfo väli ei saa sisaldada rohkem kui 5000 tähemärki")]
		public string? Info { get; set; }
	}
}
