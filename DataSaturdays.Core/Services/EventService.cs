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

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            return await _eventRepository.GetEventByIdAsync(eventId);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync()
        {
            return await _eventRepository.GetEvents();
        }


    }
}
