using Nullam.Validators;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the ID of the event
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the event
        /// </summary>
        [Required(ErrorMessage = "Palun sisestage ürituse nimi")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the occurrence time of the event
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        [Required(ErrorMessage = "Palun sisestage toimumisaeg ettenäidatud formaadis")]
        [FutureDate(ErrorMessage = "Kuupäev peab olema tulevikus")]
        public DateTime OccurrenceTime { get; set; }

        /// <summary>
        /// Gets or sets the place of the event
        /// </summary>
        [Required(ErrorMessage = "Palun sisestage toimumiskoht")]
        public required string Place { get; set; }

        /// <summary>
        /// Gets or sets additional information about the event
        /// </summary>
        [StringLength(1000, ErrorMessage = "Lisainfo väli ei saa sisaldada rohkem kui 1000 tähemärki")]
        public string? Info { get; set; }

        /// <summary>
        /// Gets or sets participating companies in the event
        /// </summary>
        public IList<Company>? Companies { get; set; }

        /// <summary>
        /// Gets or sets persons participating persons in the event.
        /// </summary>
        public IList<Person>? Persons { get; set; }
    }
}
