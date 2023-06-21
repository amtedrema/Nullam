using Microsoft.AspNetCore.Mvc;
using Nullam.Models;
using Nullam.Services;
using Nullam.ViewModels;

namespace Nullam.Controllers
{
    /// <summary>
    /// Controller for managing events
    /// </summary>
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IParticipantService _participantService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class
        /// </summary>
        /// <param name="eventService">The event service</param>
        /// <param name="participantService">The participant service</param>
        public EventController(IEventService eventService, IParticipantService participantService)
        {
            _eventService = eventService;
            _participantService = participantService;
        }

        /// <summary>
        /// Retrieves all events and displays them
        /// </summary>
        /// <returns>The view displaying the list of events</returns>
        public IActionResult Index()
        {
            var events = _eventService.GetEventsByDate();

            return View(events);
        }

        /// <summary>
        /// Displays the create event form
        /// </summary>
        /// <returns>The view for creating an event</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="obj">The event object to create</param>
        /// <returns>Creation of an event</returns>
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

        /// <summary>
        /// Retrieves the details of a specific event
        /// </summary>
        /// <param name="id">The ID of the event</param>
        /// <returns>The view displaying the event details</returns>
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

        /// <summary>
        /// Creates a participant for an event
        /// </summary>
        /// <param name="participantVM">The participant viewmodel</param>
        /// <returns>Creation of participant</returns>
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

        /// <summary>
        /// Deletes an event
        /// </summary>
        /// <param name="id">The ID of the event to delete</param>
        /// <returns>Deletion of the event</returns>
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