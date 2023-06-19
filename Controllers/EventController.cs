using Microsoft.AspNetCore.Mvc;
using Nullam.Models;
using Nullam.Services;
using Nullam.ViewModels;

namespace Nullam.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;

        public EventController(IEventService eventService, IParticipantService participantService)
        {
            _eventService = eventService;
            _participantService = participantService;
        }

        public IActionResult Index()
        {
            var events = _eventService.GetEventsByDate();

            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event obj)
        {
            if (ModelState.IsValid)
            {
                _eventService.CreateEvent(obj);

                TempData["success"] = $"Ürituse '{obj.Name}' loomine õnnestus edukalt";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Details(Guid id)
        {
            var viewModel = _eventService.GetEventDetails(id.ToString());
            if (viewModel == null)
            {
                TempData["error"] = "Niisugust üritust ei eksisteeri";
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateParticipant(ParticipantViewModel participantVM)
        {
            // When participant is a Company - FirstName and IdCode (2 times, Required and CorrectIdCodeAttribute) properties give InValid. Person is null.
            // When participant is a Person  - Name, ParticipantAmount and RegistrationCode properties give InValid. Company is null.
            if (ModelState.ErrorCount == 3)
            {
                if (_participantService.CreateParticipant(participantVM))
                {
                    TempData["success"] = "Osaleja on lisatud üritusele";
                }
                else
                {
                    TempData["error"] = "Osaleja on registreeritud juba üritusele või ei leitud ülesse üritust kuhu registreerida";
                }

                return RedirectToAction("Details", new { id = participantVM.EventId });
            }

            var viewModel = _eventService.GetEventDetails(participantVM.EventId ?? "", participantVM);

            return View("Details", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var isEventDeleted = _eventService.DeleteEvent(id);

            if (isEventDeleted)
                TempData["success"] = "Üritus on kustutatud";
            else
                TempData["error"] = "Üritust, mida soovid kustutada, ei leitud";
            
            return RedirectToAction("Index");
        }
    }
}