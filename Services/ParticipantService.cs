using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Models;
using Nullam.ViewModels;

namespace Nullam.Services
{
    /// <summary>
    /// Represents a service for managing participants in events
    /// </summary>
    public class ParticipantService : IParticipantService
    {
        private readonly NullamDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantService"/> class
        /// </summary>
        /// <param name="context">The database context</param>
        public ParticipantService(NullamDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a list of payment method types as <see cref="SelectListItem"/>
        /// </summary>
        /// <returns>A list of payment method types as <see cref="SelectListItem"/></returns>
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

        /// <summary>
        /// Retrieves a viewmodel for a participant with the specified identifier
        /// </summary>
        /// <param name="id">The identifier of the participant.</param>
        /// <returns>An instance of the <see cref="ParticipantViewModel"/> class, or null if the participant does not exist</returns>
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

        /// <summary>
        /// Creates a new participant in the specified event
        /// </summary>
        /// <param name="participantVM">The participant view model containing the participant details</param>
        /// <returns>True if the participant was successfully created, otherwise false</returns>
        public bool CreateParticipant(ParticipantViewModel participantVM)
        {
            //eventMap is for mapping the person with an event
            var eventMap = _context.Event
                .Include(x => x.Companies)
                .Include(y => y.Persons)
                .FirstOrDefault(x => x.Id.ToString() == participantVM.EventId);

            if (eventMap == null)
            {
                // Event not found
                return false;
            }

            if (participantVM.IsCompany)
            {
                // RegistrationCode is unique
                var existingCompany = _context.Company
                    .Include(x => x.Events)
                    .FirstOrDefault(y => y.RegistrationCode == participantVM.Company.RegistrationCode);

                if (existingCompany == null)
                {
                    //Create a new Company
                    var newCompany = new Company
                    {
                        Name = participantVM.Company.Name,
                        ParticipantAmount = participantVM.Company.ParticipantAmount,
                        RegistrationCode = participantVM.Company.RegistrationCode,
                        PaymentMethodTypeId = participantVM.Company.PaymentMethodTypeId,
                        CreatedDate = DateTime.UtcNow,
                        Info = participantVM.Company.Info,
                        Events = new List<Event> { eventMap }
                    };

                    _context.Company.Add(newCompany);
                    _context.SaveChanges();

                    return true;
                }
                else if (existingCompany.Events != null && existingCompany.Events.All(x => x.Id != eventMap.Id))
                {
                    // If the Company already exists in the database but hasn't participated in the event
                    existingCompany.Events.Add(eventMap);
                    _context.Company.Update(existingCompany);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    // Company already registered for the event
                    return false;
                }
            }
            else
            {
                //Idcode is unique
                var existingPerson = _context.Person
                    .Include(x => x.Events)
                    .FirstOrDefault(y => y.IdCode == participantVM.Person.IdCode);

                if (existingPerson == null)
                {
                    // Create a new Person
                    var newPerson = new Person
                    {
                        FirstName = participantVM.Person.FirstName,
                        LastName = participantVM.Person.LastName,
                        IdCode = participantVM.Person.IdCode,
                        PaymentMethodTypeId = participantVM.Person.PaymentMethodTypeId,
                        CreatedDate = DateTime.UtcNow,
                        Info = participantVM.Person.Info,
                        Events = new List<Event> { eventMap }
                    };

                    _context.Person.Add(newPerson);
                    _context.SaveChanges();

                    return true;
                }
                else if (existingPerson.Events != null && existingPerson.Events.All(x => x.Id != eventMap.Id))
                {
                    // If the Person already exists in the database but hasn't participated in the event
                    existingPerson.Events.Add(eventMap);
                    _context.Person.Update(existingPerson);
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    // Person already registered for the event
                    return false;
                }
            }
        }

        /// <summary>
        /// Updates the details of an existing participant
        /// </summary>
        /// <param name="participantVM">The participant view model containing the updated participant details</param>
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

        /// <summary>
        /// Deletes a participant from the specified event
        /// </summary>
        /// <param name="id">The identifier of the participant to delete</param>
        /// <param name="eventId">The identifier of the event</param>
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
