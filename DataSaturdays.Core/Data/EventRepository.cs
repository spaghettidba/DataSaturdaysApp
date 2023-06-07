using Dapper;
using DataSaturdays.Core.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataSaturdays.Core.Data
{
    public class EventRepository : IEventRepository
    {

        private readonly string _connectionString;

        public EventRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            const string query = @"
                SELECT 
                    E.event_id AS Id,
                    E.name,
                    E.event_date AS [Date],
                    E.virtual,
                    E.description,
                    E.registration_url AS RegistrationURL,
                    E.callforspeakers_url AS CallForSpeakersURL,
                    E.schedule_url AS ScheduleURL,
                    E.speaker_list_url AS SpeakerListURL, 
                    E.volunteer_url AS VolunteerRequestrURL, 
                    E.hide_top_logo AS HideTopLogo, 
                    E.hide_join_room AS HideJoinRoom, 
                    E.open_registration_new_tab AS OpenRegistrationNewTab, 
                    E.schedule_app AS ScheduleApp, 
                    E.venue_map AS VenueMap, 
                    E.code_of_conduct AS CodeOfConduct, 
                    E.sponsor_benefits AS SponsorBenefits, 
                    E.sponsor_menuitem AS SponsorMenuItem, 
                    E.sponsorpack_link AS SponsorPackLink
                FROM Events AS E
                WHERE E.event_id = @event_id
                ORDER BY E.event_date DESC
            ";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Event>(query, new { eventId });
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            const string query = @"
                SELECT 
                    E.event_id AS Id,
                    E.name,
                    E.event_date AS [Date],
                    E.virtual,
                    E.description,
                    E.registration_url AS RegistrationURL,
                    E.callforspeakers_url AS CallForSpeakersURL,
                    E.schedule_url AS ScheduleURL,
                    E.speaker_list_url AS SpeakerListURL, 
                    E.volunteer_url AS VolunteerRequestrURL, 
                    E.hide_top_logo AS HideTopLogo, 
                    E.hide_join_room AS HideJoinRoom, 
                    E.open_registration_new_tab AS OpenRegistrationNewTab, 
                    E.schedule_app AS ScheduleApp, 
                    E.venue_map AS VenueMap, 
                    E.code_of_conduct AS CodeOfConduct, 
                    E.sponsor_benefits AS SponsorBenefits, 
                    E.sponsor_menuitem AS SponsorMenuItem, 
                    E.sponsorpack_link AS SponsorPackLink
                FROM Events AS E
                ORDER BY E.event_date DESC
            ";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Event>(query);
        }
    }
}
