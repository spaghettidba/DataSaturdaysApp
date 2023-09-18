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
        Task<IEnumerable<Event>> GetEventsByUserNameAsync(string userName);
        Task<Guid> CreateEvent(Event Input);
        Task CreateMilestone(Milestone Input);
        Task CreateOrganizer(Organizer Input);
        Task CreateSponsor(Sponsor Input);
        Task CreateRoom(Room Input);
        Task CreatePrecon(Preconference Input);
        Task CreateSpeaker(Speaker Input);
        Task UpdateEvent(Event Input);
        Task UpdateMilestone(Milestone Input);
        Task UpdateOrganizer(Organizer Input);
        Task UpdateSponsor(Sponsor Input);
        Task UpdateRoom(Room Input);
        Task UpdatePrecon(Preconference Input);
        Task UpdateSpeaker(Speaker Input);
        Task DeleteMilestone(Milestone Input);
        Task DeleteOrganizer(Organizer Input);
        Task DeleteRoom(Room Input);
        Task DeleteSponsor(Sponsor Input);
        Task DeletePrecon(Preconference Input);
        Task DeleteSpeaker(Speaker Input);
        Task<IEnumerable<Event>> GetEventsByUser(Guid userId);
    }
}
