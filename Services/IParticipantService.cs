using Microsoft.AspNetCore.Mvc.Rendering;
using Nullam.ViewModels;

namespace Nullam.Services
{
    /// <summary>
    /// Represents a service for managing participants
    /// </summary>
    public interface IParticipantService
    {
        /// <summary>
        /// Retrieves a list of payment method types as select list items
        /// </summary>
        /// <returns>A list of select list items representing payment method types</returns>
        List<SelectListItem> GetPaymentMethodTypes();

        /// <summary>
        /// Retrieves a participant view model based on the specified identifier
        /// </summary>
        /// <param name="id">The identifier of the participant</param>
        /// <returns>The participant view model if found; otherwise, null</returns>
        ParticipantViewModel? GetParticipantViewModel(Guid id);

        /// <summary>
        /// Creates a new participant based on the provided participant view model
        /// </summary>
        /// <param name="participantVM">The participant view model containing the participant details</param>
        /// <returns>True if the participant was created successfully; otherwise, false</returns>
        bool CreateParticipant(ParticipantViewModel participantVM);

        /// <summary>
        /// Updates the details of an existing participant
        /// </summary>
        /// <param name="participantVM">The participant view model containing the updated participant details</param>
        void UpdateParticipant(ParticipantViewModel participantVM);

        /// <summary>
        /// Deletes a participant from the specified event
        /// </summary>
        /// <param name="id">The identifier of the participant to delete</param>
        /// <param name="eventId">The identifier of the event</param>
        void DeleteParticipant(Guid id, string? eventId);
    }
}
