﻿using DataSaturdays.Core.Entities;
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
    }
}
