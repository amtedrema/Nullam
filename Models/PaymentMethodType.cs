namespace Nullam.Models
{
    public class PaymentMethodType
    {
		public int Id { get; set; }
        public required string Name { get; set; }
        public required string LabelText { get; set; }
	}
}
