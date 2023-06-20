using Nullam.Validators;
using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Palun sisestage ürituse nimi")]
        public required string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}")]
        [Required(ErrorMessage = "Palun sisestage toimumisaeg ettenäidatud formaadis")]
        [FutureDate(ErrorMessage = "Kuupäev peab olema tulevikus")]
        public DateTime OccurrenceTime { get; set; }

        [Required(ErrorMessage = "Palun sisestage toimumiskoht")]
        public required string Place { get; set; }

        [StringLength(1000, ErrorMessage = "Lisainfo väli ei saa sisaldada rohkem kui 1000 tähemärki")]
        public string? Info { get; set; }
        public IList<Company>? Companies { get; set; }
        public IList<Person>? Persons { get; set; }
    }
}
