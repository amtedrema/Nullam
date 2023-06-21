using System.ComponentModel.DataAnnotations;

namespace Nullam.Models
{
    /// <summary>
    /// Represents the base class for a participant
    /// </summary>
    public class ParticipantBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the participant
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the payment method type ID for the participant
        /// </summary>
        [Required(ErrorMessage = "Palun sisestage maksmisviis")]
        public int PaymentMethodTypeId { get; set; }

        /// <summary>
        /// Gets or sets the payment method type for the participant
        /// </summary>
        public PaymentMethodType? PaymentMethodType { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the participant
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the collection of events associated with the participant
        /// </summary>
        public ICollection<Event>? Events { get; set; }
    }
}
