using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string RegistrationURL { get; set; }
        public string ScheduleURL { get; set; }
        public string SpeakerListURL { get; set; }
        public string VolunteerRequestURL { get; set; }
    }
}
