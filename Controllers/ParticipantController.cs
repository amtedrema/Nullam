using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
					IsCompany = false,
					Person = new Person {
						Id = person.Id,
						FirstName = person.FirstName,
						LastName = person.LastName,
						IdCode = person.IdCode,
						PaymentMethodTypeId = person.PaymentMethodTypeId,
						Info = person.Info,
					}
				};
			}

			var company = _context.Company.FirstOrDefault(x => x.Id == id);
			if (company != null)
			{
				return new ParticipantViewModel
				{
					IsCompany = true,
					Company = new Company
					{
						Id = company.Id,
						Name = company.Name,
						RegistrationCode = company.RegistrationCode,
						ParticipantAmount = company.ParticipantAmount,
						PaymentMethodTypeId = company.PaymentMethodTypeId,
						Info = company.Info,
					},					
				};
			}

            return null;
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(ParticipantViewModel participantVM)
		{
			// Person FirstName and IdCode (2 validations) properties give InValid, because our participant is Company.
			if (participantVM.IsCompany && ModelState.ErrorCount == 3)
			{
				var existingCompany = _context.Company.Where(x => x.Id == participantVM.Company.Id).First();
				existingCompany.Name = participantVM.Company.Name;
				existingCompany.RegistrationCode = participantVM.Company.RegistrationCode;
				existingCompany.ParticipantAmount = participantVM.Company.ParticipantAmount;
				existingCompany.PaymentMethodTypeId = participantVM.Company.PaymentMethodTypeId;
				existingCompany.Info = participantVM.Company.Info;

				_context.Company.Update(existingCompany); 
				_context.SaveChanges();
				TempData["success"] = "Osaleja info muutmine õnnestus edukalt";

				return RedirectToAction("Details", "Event", new { id = participantVM.EventId });
			}
			// Company Name, ParticipantAmount and RegistrationCode properties give InValid, because our participant is Person.
			else if (!participantVM.IsCompany && ModelState.ErrorCount == 3)
			{
				var existingPerson = _context.Person.Where(x => x.Id == participantVM.Person.Id).First();

				existingPerson.FirstName = participantVM.Person.FirstName;
				existingPerson.LastName = participantVM.Person.LastName;
				existingPerson.IdCode = participantVM.Person.IdCode;
				existingPerson.PaymentMethodTypeId = participantVM.Person.PaymentMethodTypeId;
				existingPerson.Info = participantVM.Person.Info;

				_context.Person.Update(existingPerson);
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

		[HttpPost]
		public IActionResult NewParticipantForm(string modelJson)
		{
			var selectedList = _context.PaymentMethodType.Select(a =>
							  new SelectListItem
							  {
								  Value = a.Id.ToString(),
								  Text = a.LabelText
							  }).ToList();

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
