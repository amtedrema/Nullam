using Microsoft.AspNetCore.Mvc.Rendering;
using Nullam.ViewModels;

namespace Services
{
    public interface IParticipantService
    {
        List<SelectListItem> GetPaymentMethodTypes();
        ParticipantViewModel? GetParticipantViewModel(Guid id);
        void UpdateParticipant(ParticipantViewModel participantVM);
        void DeleteParticipant(Guid id, string? eventId);
    }
}