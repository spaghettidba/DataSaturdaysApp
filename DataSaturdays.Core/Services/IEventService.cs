using DataSaturdays.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync();
        Task<IEnumerable<Event>> GetEventsByUserAsync(Guid userId);
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task CreateEvent(Event Input);
        Task UpdateEvent(Event Input);
    }
}
