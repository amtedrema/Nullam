using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nullam.ViewModels;
using Services;

namespace Nullam.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

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
