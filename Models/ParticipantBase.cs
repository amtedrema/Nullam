using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
    public class ParticipantBase
    {
		public Guid Id { get; set; }
        [Required(ErrorMessage = "Palun sisestage maksmisviis")]
        public int PaymentMethodTypeId { get; set; }
		public PaymentMethodType? PaymentMethodType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
