using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nullam.Services;
using Nullam.ViewModels;

namespace Nullam.Controllers
{
    /// <summary>
    /// Controller for managing participants
    /// </summary>
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantController"/> class
        /// </summary>
        /// <param name="participantService">The participant service</param>
        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        /// <summary>
        /// Displays the update participant form
        /// </summary>
        /// <param name="id">The ID of the participant</param>
        /// <returns>The view for updating a participant</returns>
        public IActionResult Update(Guid id)
        {
            var selectedList = _participantService.GetPaymentMethodTypes();
            ViewBag.PaymentMethodTypeList = selectedList;

            var participantVM = _participantService.GetParticipantViewModel(id);

            if (participantVM == null)
            {
                TempData["error"] = "Niisugust osalejat ei eksisteeri ühelgi üritusel";
                return RedirectToAction("Index", "Event");
            }

            if (TempData.ContainsKey("eventId"))
                participantVM.EventId = TempData["eventId"]?.ToString();

            return View(participantVM);
        }

        /// <summary>
        /// Updates a participant
        /// </summary>
        /// <param name="participantVM">The participant viewmodel</param>
        /// <returns>Updated participant</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ParticipantViewModel participantVM)
        {
            // When participant is a Company - FirstName and IdCode (2 times, Required and CorrectIdCodeAttribute) properties give InValid. Person is null.
            // When participant is a Person  - Name, ParticipantAmount and RegistrationCode properties give InValid. Company is null.
            if (ModelState.ErrorCount == 3)
            {
                _participantService.UpdateParticipant(participantVM);

                TempData["success"] = "Osaleja info muutmine õnnestus edukalt";
                return RedirectToAction("Details", "Event", new { id = participantVM.EventId });
            }

            var selectedList = _participantService.GetPaymentMethodTypes();
            ViewBag.PaymentMethodTypeList = selectedList;

            return View(participantVM);
        }

        /// <summary>
        /// Retrieves the new participant form
        /// </summary>
        /// <param name="modelJson">The serialized model JSON</param>
        /// <returns>The partial view for the new participant form</returns>
        [HttpPost]
        public IActionResult NewParticipantForm(string modelJson)
        {
            var selectedList = _participantService.GetPaymentMethodTypes();
            ViewBag.PaymentMethodTypeList = selectedList;

            var model = JsonConvert.DeserializeObject<ParticipantViewModel>(modelJson);

            if (model != null && model.IsCompany)
                return PartialView("_CompanyForm", model);

            return PartialView("_PersonForm", model);
        }

        /// <summary>
        /// Deletes a participant
        /// </summary>
        /// <param name="id">The ID of the participant to delete</param>
        /// <returns>Deletion of the participant</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            string? eventId = TempData.ContainsKey("eventId") ? TempData["eventId"]?.ToString() : null;
            
            _participantService.DeleteParticipant(id, eventId);
                
            TempData["success"] = "Osaleja on eemaldatud ürituselt";

            return RedirectToAction("Details", "Event", new { id = eventId });
        }
    }
}
