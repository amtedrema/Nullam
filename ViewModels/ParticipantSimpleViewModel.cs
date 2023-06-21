namespace Nullam.ViewModels
{
    /// <summary>
    /// Represents a simplified view model for a participant
    /// </summary>
    public class ParticipantSimpleViewModel
    {
        /// <summary>
        /// Gets or sets the ID of the participant, which can be either the company ID or the person ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the participant, which can be either the company name or the person's full name
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Gets or sets the code associated with the participant, which can be either the person's ID code or the company's registration code
        /// </summary>
        public double? Code { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the participant for ordering purposes
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the participant is a company
        /// </summary>
        public bool IsCompany { get; set; }
    }
}
