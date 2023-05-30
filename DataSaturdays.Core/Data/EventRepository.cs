using DataSaturdays.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataSaturdays.Core.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly IMSContext _db;
        public EventRepository(IMSContext db)
        {
            _db = db;
            _db.Database.EnsureCreated();
        } 

        public async Task CreateEvent(Event Input)
        {
            Event newEvent = new Event();
            try {
                Input.Id = Guid.NewGuid();
                Input.Date = DateTime.Now;
                
                newEvent.Id = Input.Id;
                newEvent.Title = Input.Title;
                newEvent.Description = Input.Description;
                newEvent.EventName = Input.EventName;
                newEvent.Date = Input.Date;
                newEvent.RegistrationURL = Input.RegistrationURL;
                newEvent.ScheduleURL = Input.ScheduleURL;
                newEvent.SpeakerListURL = Input.SpeakerListURL;
                newEvent.VolunteerRequestURL = Input.VolunteerRequestURL;
                _db.Add(Input);
                await _db.SaveChangesAsync();
                
            }
            catch(Exception ex)
            {
                throw;
            }
            //events.Add(Input);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await _db.Events.ToListAsync();
        }
    }
}
