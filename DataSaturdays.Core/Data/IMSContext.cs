using DataSaturdays.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSaturdays.Core.Data
{
    public class IMSContext:DbContext
    {
        public IMSContext(DbContextOptions<IMSContext> options): base(options)
        {

        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event()
                {
                    Id = Guid.NewGuid(),
                    Title = "",
                    EventName = "Data Saturday Gothernburg 2022",
                    Date = new DateTime(2022, 09, 03),
                    Description = "",
                    RegistrationURL = "https://github.com/dataplat/DataSaturdays/issues/new?assignees=SQLDbaWithABeard%2CSpaghettiDba&labels=New+Event&template=new_event.yml",
                    ScheduleURL = "https://sessionize.com/api/v2/APITAG/view/GridSmat",
                    SpeakerListURL = "https://sessionize/data-saturday-parma-2022/",
                    VolunteerRequestURL = "",
                },
            new Event()
            {
                Id = Guid.NewGuid(),
                Title = "",
                EventName = "Data Saturday Atlanta",
                Date = new DateTime(2022, 10, 01),
                Description = "",
                RegistrationURL = "https://github.com/dataplat/DataSaturdays/issues/new?assignees=SQLDbaWithABeard%2CSpaghettiDba&labels=New+Event&template=new_event.yml",
                ScheduleURL = "https://sessionize.com/api/v2/APITAG/view/GridSmat",
                SpeakerListURL = "https://sessionize/data-saturday-parma-2022/",
                VolunteerRequestURL = "",
            });
        }
    }
}
