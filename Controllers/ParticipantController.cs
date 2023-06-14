using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Models;
using Nullam.ViewModels;

namespace Nullam.Controllers
{
	public class ParticipantController : Controller
	{
		private readonly NullamDbContext _context;
		public ParticipantController(NullamDbContext context)
		{
			_context = context;
		}

		public IActionResult Update(Guid id)
		{
			var selectedList = _context.PaymentMethodType.Select(a =>
								  new SelectListItem
								  {
									  Value = a.Id.ToString(),
									  Text = a.LabelText
								  }).ToList();

			ViewBag.PaymentMethodTypeList = selectedList;

			var participantVM = GetParticipantViewModel(id);

			if (participantVM == null)
			{
                TempData["error"] = "Niisugust osalejat ei eksisteeri ühelgi üritusel";
				return RedirectToAction("Index", "Event");
            }
			
			if (TempData.ContainsKey("eventId"))
				participantVM.EventId = TempData["eventId"]?.ToString();

			return View(participantVM);
		}

		private ParticipantViewModel? GetParticipantViewModel(Guid id)
		{
			var person = _context.Person.FirstOrDefault(x => x.Id == id);
			if (person != null)
			{
				return new ParticipantViewModel
				{
					Id = person.Id,
					FirstName = person.FirstName,
					LastName = person.LastName,
					IdCode = person.IdCode,
					PaymentMethodTypeId = person.PaymentMethodTypeId,
					Info = person.Info,
					IsCompany = false
				};
			}

			var company = _context.Company.FirstOrDefault(x => x.Id == id);
			if (company != null)
			{
				return new ParticipantViewModel
				{
					Id = company.Id,
					Name = company.Name,
					RegistrationCode = company.RegistrationCode,
					ParticipantAmount = company.ParticipantAmount,
					PaymentMethodTypeId = company.PaymentMethodTypeId,
					Info = company.Info,
					IsCompany = true
				};
			}

            return null;
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(ParticipantViewModel participantVM)
        {
			if (ModelState.IsValid)
			{
				if (participantVM.IsCompany)
				{
                    var existingCompany = _context.Company.Where(x => x.Id == participantVM.Id).First();
					existingCompany.Name = participantVM.Name ?? "";
					existingCompany.RegistrationCode = participantVM.RegistrationCode ?? -1;
					existingCompany.ParticipantAmount = participantVM.ParticipantAmount ?? 0;
					existingCompany.PaymentMethodTypeId = participantVM.PaymentMethodTypeId;
					existingCompany.Info = participantVM.Info;

                    _context.Company.Update(existingCompany);
                }
				else
				{
                    var existingPerson = _context.Person.Where(x => x.Id == participantVM.Id).First();

                    existingPerson.FirstName = participantVM.FirstName ?? "";
                    existingPerson.LastName = participantVM.LastName ?? "";
                    existingPerson.IdCode = participantVM.IdCode ?? -1;
                    existingPerson.PaymentMethodTypeId = participantVM.PaymentMethodTypeId;
                    existingPerson.Info = participantVM.Info;

                    _context.Person.Update(existingPerson);
                }

                _context.SaveChanges();
                TempData["success"] = "Osaleja info muutmine õnnestus edukalt";

                return RedirectToAction("Details", "Event", new { id = participantVM.EventId });
			}

			var selectedList = _context.PaymentMethodType.Select(a =>
								  new SelectListItem
								  {
									  Value = a.Id.ToString(),
									  Text = a.LabelText
								  }).ToList();

			ViewBag.PaymentMethodTypeList = selectedList;

			return View(participantVM);
        }

        public IActionResult PersonForm()
		{
            var participantVM = new ParticipantViewModel { IsCompany = false };
            var selectedList = _context.PaymentMethodType.Select(a =>
								  new SelectListItem
								  {
									  Value = a.Id.ToString(),
									  Text = a.LabelText
								  }).ToList();

			ViewBag.PaymentMethodTypeList = selectedList;

            return PartialView("_PersonForm", participantVM);
		}

		public IActionResult CompanyForm()
		{
			var participantVM = new ParticipantViewModel {	IsCompany = true };

			var selectedList = _context.PaymentMethodType.Select(a =>
							  new SelectListItem
							  {
								  Value = a.Id.ToString(),
								  Text = a.LabelText
							  }).ToList();

			ViewBag.PaymentMethodTypeList = selectedList;

            return PartialView("_CompanyForm", participantVM);
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ParticipantViewModel participantVM)
		{
            string? eventId = null;
            if (TempData.ContainsKey("eventId"))
            {
                eventId = TempData["eventId"]?.ToString();
            }

            if ((!participantVM.IsCompany && ModelState.ErrorCount == 2) || (participantVM.IsCompany && ModelState.ErrorCount == 3))
			{
				Event eventMap;
				if (eventId != null)
				{
					eventMap = _context.Event.First(x => x.Id.ToString() == eventId);
				}
				else
				{
					TempData["error"] = "Ei leia üles üritust millega osalejat siduda";
					return RedirectToAction("Index", "Event");
				}

				if (participantVM.IsCompany)
				{
					var existingCompany = _context.Company.Include(x => x.Events).Where(x => x.RegistrationCode == participantVM.RegistrationCode).FirstOrDefault();
					if (existingCompany == null)
					{
						var newCompany = new Company
						{
							Name = participantVM.Name ?? "",
							ParticipantAmount = participantVM.ParticipantAmount ?? -1,
							RegistrationCode = participantVM.RegistrationCode ?? -1,
							PaymentMethodTypeId = participantVM.PaymentMethodTypeId,
							CreatedDate = DateTime.UtcNow,
							Info = participantVM.Info,
							Events = new List<Event>()
						};

						newCompany.Events.Add(eventMap);
						_context.Company.Add(newCompany);
					}
					else if (existingCompany.Events != null && existingCompany.Events.All(x => x.Id != eventMap.Id))
					{
						existingCompany.Events.Add(eventMap);
						_context.Company.Update(existingCompany);
					}
					else
					{
						TempData["error"] = "Ettevõte registrikoodiga '" + existingCompany.RegistrationCode + "' on juba registreerinud";
						return RedirectToAction("Details", "Event", new { id = eventId });
					}
				}
				else
				{
					var existingPerson = _context.Person.Include(x => x.Events).Where(y => y.IdCode == participantVM.IdCode).FirstOrDefault();
					if (existingPerson == null)
					{
						var newPerson = new Person
						{
							FirstName = participantVM.FirstName ?? "",
							LastName = participantVM.LastName ?? "",
							IdCode = participantVM.IdCode ?? -1,
							PaymentMethodTypeId = participantVM.PaymentMethodTypeId,
							CreatedDate = DateTime.UtcNow,
							Info = participantVM.Info,
							Events = new List<Event>()
						};

						newPerson.Events.Add(eventMap);
						_context.Person.Add(newPerson);
					}
					else if (existingPerson.Events != null && existingPerson.Events.All(x => x.Id != eventMap.Id))
					{
						existingPerson.Events.Add(eventMap);
						_context.Person.Update(existingPerson);
					}
					else
					{
						TempData["error"] = "Osaleja isikukoodiga '" + existingPerson.IdCode + "' on end juba registreerinud";
						return RedirectToAction("Details", "Event", new { id = eventId });
					}
				}

				_context.SaveChanges();
				TempData["success"] = "Osaleja lisamine õnnestus edukalt";
                return RedirectToAction("Details", "Event", new { id = eventId });
            }

            TempData["error"] = "Osaleja lisamine ebaõnnestus";
            return RedirectToAction("Details", "Event", new { id = eventId });
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Guid id)
		{
			string? eventId = null;
			if (TempData.ContainsKey("eventId"))
			{
				eventId = TempData["eventId"]?.ToString();
			}

			var companyToDelete = _context.Company.Include(x => x.Events).FirstOrDefault(z => z.Id == id);

			if (companyToDelete != null)
			{
				if(companyToDelete.Events?.Count == 1)
				{
					_context.Company.Remove(companyToDelete);
				}
				else
				{
					_context.Database.ExecuteSql($"DELETE FROM EventCompany WHERE EventId = {eventId} AND CompanyId = {companyToDelete.Id};");
				}

				_context.SaveChanges();
				TempData["success"] = "Ettevõte '" + companyToDelete.Name + "' on eemaldatud ürituselt";
				return RedirectToAction("Details", "Event", new { id = eventId });
			}

			var personToDelete = _context.Person.Include(x => x.Events).FirstOrDefault(z => z.Id == id);
			if (personToDelete != null)
			{
				if (personToDelete.Events?.Count == 1)
				{
					_context.Person.Remove(personToDelete);
				}
				else
				{
					_context.Database.ExecuteSql($"DELETE FROM EventPerson WHERE EventId = {eventId} AND CompanyId = {personToDelete.Id};");
				}

				TempData["success"] = "Isik '" + personToDelete.FirstName + " " + personToDelete.LastName + "' on eemaldatud ürituselt";
				_context.SaveChanges();
			}
			else
			{
				TempData["error"] = "Kustutamine ebaõnnestus";
			}

			return RedirectToAction("Details", "Event", new { id = eventId });
		}
	}
}
