using Microsoft.EntityFrameworkCore;
using Nullam.Data;
using Nullam.Models;
using Nullam.ViewModels;

namespace Nullam.Services
{
    public class EventService : IEventService
    {
        private readonly NullamDbContext _context;

        public EventService(NullamDbContext context)
        {
            _context = context;
        }

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

        public void CreateEvent(Event obj)
        {
            if (obj != null)
            {
                _context.Event.Add(obj);
                _context.SaveChanges();
            }
        }

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
