using DataSaturdays.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Data
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task CreateEvent(Event Input);
        Task UpdateEvent(Event Input);
        Task<IEnumerable<Event>> GetEventsByUser(Guid userId);
    }
}
