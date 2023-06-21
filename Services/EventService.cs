using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Models;
using Nullam.ViewModels;

namespace Nullam.Services
{
    /// <summary>
    /// Provides methods to interact with events in the application
    /// </summary>
    public class EventService : IEventService
    {
        private readonly NullamDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class with the specified database context
        /// </summary>
        /// <param name="context">The database context</param>
        public EventService(NullamDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a viewmodel containing events grouped by date
        /// </summary>
        /// <returns>An instance of the <see cref="EventsByDateViewModel"/> class</returns>
        public EventsByDateViewModel GetEventsByDate()
        {
            var events = _context.Event.ToList();
            var pastEvents = events.Where(x => x.OccurrenceTime < DateTime.UtcNow).OrderByDescending(x => x.OccurrenceTime).ToList();
            var futureEvents = events.Except(pastEvents).OrderBy(x => x.OccurrenceTime).ToList();

            var viewModel = new EventsByDateViewModel
            {
                PastEvents = pastEvents,
                FutureEvents = futureEvents
            };

            return viewModel;
        }

        /// <summary>
        /// Retrieves detailed information about an event
        /// </summary>
        /// <param name="id">The identifier of the event</param>
        /// <param name="participant">An optional participant viewmodel</param>
        /// <returns>An instance of the <see cref="EventDetailViewModel"/> class, or null if the event does not exist</returns>
        public EventDetailViewModel? GetEventDetails(string id, ParticipantViewModel? participant = null)
        {
            var eve = _context.Event
                .Include(x => x.Companies)
                .Include(y => y.Persons)
                .FirstOrDefault(x => x.Id.ToString() == id);

            if (eve == null)
                return null;

            var participantList = new List<ParticipantSimpleViewModel>();

            foreach (var item in eve.Persons ?? Enumerable.Empty<Person>())
            {
                participantList.Add(new ParticipantSimpleViewModel
                {
                    Id = item.Id,
                    FullName = $"{item.FirstName} {item.LastName}",
                    Code = item.IdCode,
                    CreatedDate = item.CreatedDate,
                    IsCompany = false
                });
            }

            foreach (var item in eve.Companies ?? Enumerable.Empty<Company>())
            {
                participantList.Add(new ParticipantSimpleViewModel
                {
                    Id = item.Id,
                    FullName = item.Name,
                    Code = item.RegistrationCode,
                    CreatedDate = item.CreatedDate,
                    IsCompany = true
                });
            }

            participantList = participantList.OrderBy(x => x.CreatedDate).ToList();

            var newParticipant = new ParticipantViewModel
            {
                EventId = eve.Id.ToString()
            };

            if (participant != null)
            {
                newParticipant = participant;
            }

            var viewModel = new EventDetailViewModel
            {
                Event = eve,
                Participants = participantList,
                NewParticipant = newParticipant
            };

            return viewModel;
        }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="obj">The event object to create</param>
        public void CreateEvent(Event obj)
        {
            if (obj != null)
            {
                _context.Event.Add(obj);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an event with the specified identifier
        /// </summary>
        /// <param name="id">The identifier of the event to delete</param>
        /// <returns>True if the event was successfully deleted, otherwise false</returns>
        public bool DeleteEvent(Guid id)
        {
            var eventToDelete = _context.Event
                .Include(x => x.Companies)
                .Include(y => y.Persons)
                .FirstOrDefault(z => z.Id == id);

            if (eventToDelete == null)
                return false;

            foreach (var item in eventToDelete.Companies ?? Enumerable.Empty<Company>())
            {
                var tempCompany = _context.Company
                    .Include(x => x.Events)
                    .First(x => x.Id == item.Id);

                //If this company have only 1 event what you want to delete, then also delete the company
                if (tempCompany.Events?.Count == 1)
                {
                    _context.Company.Remove(tempCompany);
                }
            }

            foreach (var item in eventToDelete.Persons ?? Enumerable.Empty<Person>())
            {
                var tempPerson = _context.Person
                    .Include(x => x.Events)
                    .First(x => x.Id == item.Id);

                //If this person have only 1 event what you want to delete, then also delete the person
                if (tempPerson.Events?.Count == 1)
                {
                    _context.Person.Remove(tempPerson);
                }
            }

            _context.Event.Remove(eventToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}
