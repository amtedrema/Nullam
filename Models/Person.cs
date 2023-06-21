using Nullam.Validators;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
    /// <summary>
    /// Represents a person who is a participant
    /// </summary>
    public class Person : ParticipantBase
    {
        /// <summary>
        /// Gets or sets the required first name of the person
        /// </summary>
        [Required(ErrorMessage = "Palun sisestage eesnimi")]
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the required identification code of the person
        /// </summary>
        [Required(ErrorMessage = "Palun sisestage isikukood")]
        [CorrectIdCode(ErrorMessage = "Isikukood ei vasta Eesti Vabariigi seatud standarditele")]
        [Range(10000000000, 99999999999, ErrorMessage = "Eesti isikukood sisaldab 11 numbrit")]
        public long? IdCode { get; set; }

        /// <summary>
        /// Gets or sets the additional information about the person
        /// </summary>
        [StringLength(1500, ErrorMessage = "Lisainfo väli ei saa sisaldada rohkem kui 1500 tähemärki")]
        public string? Info { get; set; }
    }
}
