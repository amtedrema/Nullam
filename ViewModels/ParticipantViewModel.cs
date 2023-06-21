using Nullam.Models;

namespace Nullam.ViewModels
{
    /// <summary>
    /// Represents a view model for a participant
    /// </summary>
    public class ParticipantViewModel
    {
        /// <summary>
        /// Gets or sets the company associated with the participant
        /// </summary>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the person associated with the participant
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the participant is a company
        /// </summary>
        public bool IsCompany { get; set; }

        /// <summary>
        /// Gets or sets the ID of the event associated with the participant
        /// </summary>
        public string? EventId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantViewModel"/> class
        /// </summary>
        public ParticipantViewModel()
        {
            Company = new Company() { Name = "" };
            Person = new Person() { FirstName = "" };
        }
    }
}
