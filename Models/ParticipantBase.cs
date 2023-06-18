
namespace Nullam.Models
{
    public class ParticipantBase
    {
		public Guid Id { get; set; }
        public int PaymentMethodTypeId { get; set; }
		public PaymentMethodType? PaymentMethodType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public ICollection<Event>? Events { get; set; }
    }
}
