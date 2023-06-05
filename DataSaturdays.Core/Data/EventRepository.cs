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

        public EventRepository()
        {

        } 

        public async Task CreateEvent(Event Input)
        {
            Event newEvent = new Event();
            try {
                Input.Id = Guid.NewGuid();
                Input.Date = DateTime.Now;
                
                newEvent.Id = Input.Id;
                newEvent.Description = Input.Description;
                newEvent.Name = Input.Name;
                newEvent.Date = Input.Date;
                newEvent.RegistrationURL = Input.RegistrationURL;
                newEvent.ScheduleURL = Input.ScheduleURL;
                newEvent.SpeakerListURL = Input.SpeakerListURL;
                newEvent.VolunteerRequestURL = Input.VolunteerRequestURL;

                throw new NotImplementedException("Not Implemented");

            }
            catch(Exception ex)
            {
                throw;
            }
            //events.Add(Input);
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            throw new NotImplementedException("Not Implemented");
        }
    }
}
