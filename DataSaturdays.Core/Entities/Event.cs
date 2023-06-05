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
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public bool Virtual { get; set; }
        public Uri? RegistrationURL { get; set; }
        public Uri? CallForSpeakersURL { get; set; }
        public Uri? ScheduleURL { get; set; }
        public Uri? SpeakerListURL { get; set; }
        public Uri? VolunteerRequestURL { get; set; }
        public bool HideTopLogo { get; set; }
        public bool HideJoinRoom { get; set; }
        public bool OpenRegistrationNewTab { get; set; }

        public Uri? ScheduleApp { get; set; }
        public Uri? VenueMap { get; set; }

        public string CodeOfConduct { get; set; }
        public string SponsorBenefits { get; set; }
        public bool SponsorMenuItem { get; set; }
        public Uri? SponsorPackLink { get; set; }

        public List<Room>? Rooms { get; set; }
        public List<Milestone>? Milestones { get; set; }
        public List<Sponsor>? Sponsors { get; set; }
        public List<Precon>? Precons { get; set; }
    }
}

