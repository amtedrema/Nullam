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

            var participantList = new List<ParticipantSimpleViewModel>();

            foreach (var item in eve.Persons ?? Enumerable.Empty<Person>())
            {
                participantList.Add(
                    new ParticipantSimpleViewModel
					{
                        Id = item.Id,
                        FullName = item.FirstName + " " + item.LastName,
                        Code = item.IdCode,
                        CreatedDate = item.CreatedDate,
                        IsCompany = false
                    }
                );
            }

            foreach (var item in eve.Companies ?? Enumerable.Empty<Company>())
            {
                participantList.Add(
                    new ParticipantSimpleViewModel
					{
                        Id = item.Id,
                        FullName = item.Name,
                        Code = item.RegistrationCode,
                        CreatedDate = item.CreatedDate,
                        IsCompany = true
                    }
                );
            }

            participantList = participantList.OrderBy(x => x.CreatedDate).ToList();

            var newParticipant = new ParticipantViewModel 
            { 
                EventId = eve.Id.ToString() 
            };

			return View(new EventDetailViewModel { Event = eve, Participants = participantList, NewParticipant = newParticipant });
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateParticipant(ParticipantViewModel participantVM)
		{
			//EventMap is for a mapping the person with an event
			Event eventMap;
			if (participantVM.EventId != null)
			{
				eventMap = _context.Event
					.Include(x => x.Companies)
					.Include(y => y.Persons)
					.First(x => x.Id.ToString() == participantVM.EventId);
			}
			else
			{
				TempData["error"] = "Ei leia üles üritust millega osalejat siduda";
				return RedirectToAction("Index", "Event");
			}

			// Person FirstName and IdCode properties give InValid, because our participant is Company.
			if (participantVM.IsCompany && ModelState.ErrorCount == 3)
			{
				var existingCompany = _context.Company.Include(x => x.Events).Where(y => y.RegistrationCode == participantVM.Company.RegistrationCode).FirstOrDefault();

				//Create a new Company
				if (existingCompany == null)
				{
					var newCompany = new Company
					{
						Name = participantVM.Company.Name,
						ParticipantAmount = participantVM.Company.ParticipantAmount,
						RegistrationCode = participantVM.Company.RegistrationCode,
						PaymentMethodTypeId = participantVM.Company.PaymentMethodTypeId,
						CreatedDate = DateTime.UtcNow,
						Info = participantVM.Company.Info,
						Events = new List<Event>()
					};


					newCompany.Events.Add(eventMap);
					_context.Company.Add(newCompany);
					TempData["success"] = "Ettevõte '" + newCompany.Name + "' on lisatud üritusele";
				}

				//If the Company already exists in the database, but haven't participate for the event
				else if (existingCompany.Events != null && existingCompany.Events.All(x => x.Id != eventMap.Id))
				{
					existingCompany.Events.Add(eventMap);
					_context.Company.Update(existingCompany);
					TempData["success"] = "Ettevõte registrikoodiga '" + existingCompany.RegistrationCode + "' on registreeritud üritusele";
				}

				else
				{
					TempData["error"] = "Ettevõte registrikoodiga '" + existingCompany.RegistrationCode + "' on juba registreerinud";
				}

				_context.SaveChanges();
				participantVM = new ParticipantViewModel
				{
					EventId = eventMap.Id.ToString()
				};
			}
			// Company Name, ParticipantAmount and RegistrationCode properties give InValid, because our participant is Person.
			else if (!participantVM.IsCompany && ModelState.ErrorCount == 3)
			{
				//From Idcode because its unique
				var existingPerson = _context.Person.Include(x => x.Events).Where(y => y.IdCode == participantVM.Person.IdCode).FirstOrDefault();

				//Create a new Person
				if (existingPerson == null)
				{
					var newPerson = new Person
					{
						FirstName = participantVM.Person.FirstName,
						LastName = participantVM.Person.LastName,
						IdCode = participantVM.Person.IdCode,
						PaymentMethodTypeId = participantVM.Person.PaymentMethodTypeId,
						CreatedDate = DateTime.UtcNow,
						Info = participantVM.Person.Info,
						Events = new List<Event>()
					};

					newPerson.Events.Add(eventMap);
					_context.Person.Add(newPerson);
					TempData["success"] = "Osaleja '" + newPerson.FirstName + " " + newPerson.LastName + "' on lisatud üritusele";
				}

				//If the Person already exists in the database, but haven't participate for the event
				else if (existingPerson.Events != null && existingPerson.Events.All(x => x.Id != eventMap.Id))
				{
					existingPerson.Events.Add(eventMap);
					_context.Person.Update(existingPerson);
					TempData["success"] = "Osaleja '" + existingPerson.FirstName + " " + existingPerson.LastName + "' on lisatud üritusele";
				}
				else
				{
					TempData["error"] = "Isik Id koodiga '" + existingPerson.IdCode + "' on juba registreerinud";
				}

				_context.SaveChanges();
				participantVM = new ParticipantViewModel
				{
					EventId = eventMap.Id.ToString()
				};
			}

			var participantList = new List<ParticipantSimpleViewModel>();

			foreach (var item in eventMap.Persons ?? Enumerable.Empty<Person>())
			{
				participantList.Add(
					new ParticipantSimpleViewModel
					{
						Id = item.Id,
						FullName = item.FirstName + " " + item.LastName,
						Code = item.IdCode,
						CreatedDate = item.CreatedDate,
						IsCompany = false
					}
				);
			}

			foreach (var item in eventMap.Companies ?? Enumerable.Empty<Company>())
			{
				participantList.Add(
					new ParticipantSimpleViewModel
					{
						Id = item.Id,
						FullName = item.Name,
						Code = item.RegistrationCode,
						CreatedDate = item.CreatedDate,
						IsCompany = true
					}
				);
			}

			participantList = participantList.OrderBy(x => x.CreatedDate).ToList();

			return View("Details", new EventDetailViewModel { Event = eventMap, Participants = participantList, NewParticipant = participantVM });
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