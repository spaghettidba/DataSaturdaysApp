using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace DataSaturdays.Core.Entities
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public bool Virtual { get; set; }
        public string RegistrationURL { get; set; }
        public string CallForSpeakersURL { get; set; }
        public string ScheduleURL { get; set; }
        public string ScheduleDescription { get; set; }
        public string SpeakerListURL { get; set; }
        public string VolunteerRequestURL { get; set; }
        public bool HideTopLogo { get; set; }
        public bool HideJoinRoom { get; set; }
        public bool OpenRegistrationNewTab { get; set; }

        public string ScheduleApp { get; set; }
        public string VenueMap { get; set; }

        public string CodeOfConduct { get; set; }
        public string SponsorBenefits { get; set; }
        public bool SponsorMenuItem { get; set; }

        public string PreconDescription { get; set; }

        public List<Room>? Rooms { get; set; } = new();
        public List<Milestone>? Milestones { get; set; } = new();
        public List<Sponsor>? Sponsors { get; set; } = new();
        public List<Preconference>? Precons { get; set; } = new();
        public List<Organizer>? Organizers { get; set; } = new();
        public bool Published { get; set; }

        public string GetRegistrationTarget()
        {
            if (OpenRegistrationNewTab)
                return "_blank";
            else 
                return "_self"; 
        }
    }
}

