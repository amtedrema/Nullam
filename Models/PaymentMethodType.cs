namespace Nullam.Models
{
    /// <summary>
    /// Represents a payment method type
    /// </summary>
    public class PaymentMethodType
    {
        /// <summary>
        /// Gets or sets the unique identifier of the payment method type
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        /// Gets or sets the required name of the payment method type
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the required label text of the payment method type
        /// </summary>
        public required string LabelText { get; set; }
	}
}
