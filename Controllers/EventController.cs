using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Models;
using Nullam.ViewModels;
using System;

namespace Nullam.Controllers
{
    public class EventController : Controller
    {
        private readonly NullamDbContext _context;
        public EventController(NullamDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Event.ToList();
            var pastEvents = events.Where(x => x.OccurrenceTime < DateTime.UtcNow).OrderByDescending(x => x.OccurrenceTime).ToList();
            var futureEvents = events.Except(pastEvents).OrderBy(x => x.OccurrenceTime).ToList();

            var userFormViewModel = new EventsByDateViewModel
            {
                PastEvents = pastEvents,
                FutureEvents = futureEvents
            };
            return View(userFormViewModel);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Event obj)
        {
            if (ModelState.IsValid)
            {
                _context.Event.Add(obj);
                _context.SaveChanges();

                TempData["success"] = "Üritus '" + obj.Name + "' loomine õnnestus edukalt";

                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Details(Guid id)
        {
            var eve = _context.Event
                .Include(x => x.Companies)
                .Include(y => y.Persons)
                .FirstOrDefault(x => x.Id == id);

            if (eve == null)
            {
                TempData["error"] = "Niisugust üritust ei eksisteeri";
                return RedirectToAction("Index");
            }

            var participantList = new List<ParticipantViewModel>();

            foreach (var item in eve.Persons ?? Enumerable.Empty<Person>())
            {
                participantList.Add(
                    new ParticipantViewModel
                    {
                        Id = item.Id,
                        FullName = item.FirstName + " " + item.LastName,
                        Code = item.IdCode,
                        CreatedDate = item.CreatedDate
                    }
                );
            }

            foreach (var item in eve.Companies ?? Enumerable.Empty<Company>())
            {
                participantList.Add(
                    new ParticipantViewModel
                    {
                        Id = item.Id,
                        FullName = item.Name,
                        Code = item.RegistrationCode,
                        CreatedDate = item.CreatedDate
                    }
                );
            }

            participantList = participantList.OrderBy(x => x.CreatedDate).ToList();

            return View(new EventDetailViewModel { Event = eve, Participants = participantList });
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Guid id)
        {
            var eventToDelete = _context.Event.Include(x => x.Companies).Include(y => y.Persons).FirstOrDefault(z => z.Id == id);

            if (eventToDelete == null)
            {
                TempData["error"] = "Üritust mida soovid kustutada ei leitud ülesse";
                return RedirectToAction("Index");
            }

            foreach (var item in eventToDelete.Companies ?? Enumerable.Empty<Company>())
            {
                var tempCompany = _context.Company.Include(x => x.Events).First(x => x.Id == item.Id);
                if (tempCompany.Events?.Count == 1)
                {
                    _context.Company.Remove(tempCompany);
                }
            }

            foreach (var item in eventToDelete.Persons ?? Enumerable.Empty<Person>())
            {
                var tempPerson = _context.Person.Include(x => x.Events).First(x => x.Id == item.Id);
                if (tempPerson.Events?.Count == 1)
                {
                    _context.Person.Remove(tempPerson);
                }
            }

            _context.Event.Remove(eventToDelete);
            _context.SaveChanges();
            TempData["success"] = "Üritus '" + eventToDelete.Name + "' on kustutatud";

            return RedirectToAction("Index");
        }
    }
}