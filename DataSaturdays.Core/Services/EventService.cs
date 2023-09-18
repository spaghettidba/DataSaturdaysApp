using DataSaturdays.Core.Data;
using DataSaturdays.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Services
{
    public class EventService: IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task CreateEvent(Event Input)
        {
             await _eventRepository.CreateEvent(Input);
        }

        public async Task CreateMilestone(Milestone Input)
        {
            await _eventRepository.CreateMilestone(Input);
        }

        public async Task CreateOrganizer(Organizer Input)
        {
            await _eventRepository.CreateOrganizer(Input);
        }

        public async Task CreateRoom(Room Input)
        {
            await _eventRepository.CreateRoom(Input);
        }

        public async Task CreatePrecon(Preconference Input)
        {
            await _eventRepository.CreatePrecon(Input);
        }        
        
        public async Task CreateSpeaker(Speaker Input)
        {
            await _eventRepository.CreateSpeaker(Input);
        }        
        
        public async Task CreateSponsor(Sponsor Input)
        {
            await _eventRepository.CreateSponsor(Input);
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await _eventRepository.GetEventByIdAsync(eventId);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _eventRepository.GetEvents();
        }

        public async Task<IEnumerable<Event>> GetEventsByUserAsync(Guid userId)
        {
            return await _eventRepository.GetEventsByUser(userId);
        }

        public async Task<IEnumerable<Event>> GetEventsByUserNameAsync(string userName)
        {
            return await _eventRepository.GetEventsByUserNameAsync(userName);
        }

        public async Task UpdateEvent(Event Input)
        {
            await _eventRepository.UpdateEvent(Input);
        }

        public async Task UpdateMilestone(Milestone Input)
        {
            await _eventRepository.UpdateMilestone(Input);
        }

        public async Task UpdateOrganizer(Organizer Input)
        {
            await _eventRepository.UpdateOrganizer(Input);
        }

        public async Task UpdateRoom(Room Input)
        {
            await _eventRepository.UpdateRoom(Input);
        }

        public async Task UpdateSponsor(Sponsor Input)
        {
            await _eventRepository.UpdateSponsor(Input);
        }        
        public async Task UpdatePrecon(Preconference Input)
        {
            await _eventRepository.UpdatePrecon(Input);
        }        
        public async Task UpdateSpeaker(Speaker Input)
        {
            await _eventRepository.UpdateSpeaker(Input);
        }
        public async Task DeleteMilestone(Milestone Input)
        {
            await _eventRepository.DeleteMilestone(Input);
        }
        public async Task DeleteOrganizer(Organizer Input)
        {
            await _eventRepository.DeleteOrganizer(Input);
        }
        public async Task DeleteRoom(Room Input)
        {
            await _eventRepository.DeleteRoom(Input);
        }        
        public async Task DeleteSponsor(Sponsor Input)
        {
            await _eventRepository.DeleteSponsor(Input);
        }        
        public async Task DeletePrecon(Preconference Input)
        {
            await _eventRepository.DeletePrecon(Input);
        }        
        public async Task DeleteSpeaker(Speaker Input)
        {
            await _eventRepository.DeleteSpeaker(Input);
        }
    }
}
