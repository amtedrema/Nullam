using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Models;
using Nullam.ViewModels;
using Services;

namespace Nullam.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly NullamDbContext _context;

        public ParticipantService(NullamDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> GetPaymentMethodTypes()
        {
            return _context.PaymentMethodType
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.LabelText
                })
                .ToList();
        }

        public ParticipantViewModel? GetParticipantViewModel(Guid id)
        {
            var person = _context.Person
                .FirstOrDefault(x => x.Id == id);

            if (person != null)
            {
                return new ParticipantViewModel
                {
                    IsCompany = false,
                    Person = new Person
                    {
                        Id = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        IdCode = person.IdCode,
                        PaymentMethodTypeId = person.PaymentMethodTypeId,
                        Info = person.Info
                    }
                };
            }

            var company = _context.Company
                .FirstOrDefault(x => x.Id == id);

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
                        Info = company.Info
                    }
                };
            }

            return null;
        }

        public void UpdateParticipant(ParticipantViewModel participantVM)
        {
            if (participantVM.IsCompany)
            {
                var existingCompany = _context.Company
                    .First(x => x.Id == participantVM.Company.Id);

                existingCompany.Name = participantVM.Company.Name;
                existingCompany.RegistrationCode = participantVM.Company.RegistrationCode;
                existingCompany.ParticipantAmount = participantVM.Company.ParticipantAmount;
                existingCompany.PaymentMethodTypeId = participantVM.Company.PaymentMethodTypeId;
                existingCompany.Info = participantVM.Company.Info;

                _context.Company.Update(existingCompany);
            }
            else
            {
                var existingPerson = _context.Person
                    .First(x => x.Id == participantVM.Person.Id);

                existingPerson.FirstName = participantVM.Person.FirstName;
                existingPerson.LastName = participantVM.Person.LastName;
                existingPerson.IdCode = participantVM.Person.IdCode;
                existingPerson.PaymentMethodTypeId = participantVM.Person.PaymentMethodTypeId;
                existingPerson.Info = participantVM.Person.Info;

                _context.Person.Update(existingPerson);
            }

            _context.SaveChanges();
        }

        public void DeleteParticipant(Guid id, string? eventId)
        {
            var companyToDelete = _context.Company
                .Include(x => x.Events)
                .FirstOrDefault(z => z.Id == id);

            if (companyToDelete != null)
            {
                if (companyToDelete.Events?.Count == 1)
                {
                    _context.Company.Remove(companyToDelete);
                }
                else
                {
                    // Remove the association between the event and the company
                    _context.Database.ExecuteSql($"DELETE FROM EventCompany WHERE EventId = {eventId} AND CompanyId = {companyToDelete.Id};");
                }

                _context.SaveChanges();
            }
            else
            {
                var personToDelete = _context.Person
                    .Include(x => x.Events)
                    .FirstOrDefault(y => y.Id == id);

                if (personToDelete == null)
                    return;
                
                if (personToDelete.Events?.Count == 1)
                {
                    _context.Person.Remove(personToDelete);
                }
                else
                {
                    // Remove the association between the event and the person
                    _context.Database.ExecuteSql($"DELETE FROM EventPerson WHERE EventId = {eventId} AND PersonId = {personToDelete.Id};");
                }

                _context.SaveChanges();
            }
        }
    }
}
