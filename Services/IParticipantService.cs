using Microsoft.AspNetCore.Mvc.Rendering;
using Nullam.ViewModels;

namespace Nullam.Services
{
    public interface IParticipantService
    {
        List<SelectListItem> GetPaymentMethodTypes();
        ParticipantViewModel? GetParticipantViewModel(Guid id);
        bool CreateParticipant(ParticipantViewModel participantVM);
        void UpdateParticipant(ParticipantViewModel participantVM);
        void DeleteParticipant(Guid id, string? eventId);
    }
}