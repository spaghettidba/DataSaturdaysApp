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
            string query =
                """
                SELECT 
                    E.event_id                  AS Id,
                    E.name,
                    E.event_date                AS [Date],
                    E.virtual,
                    E.description,
                    E.registration_url          AS RegistrationURL,
                    E.callforspeakers_url       AS CallForSpeakersURL,
                    E.schedule_url              AS ScheduleURL,
                    E.speaker_list_url          AS SpeakerListURL, 
                    E.volunteer_url             AS VolunteerRequestrURL, 
                    E.hide_top_logo             AS HideTopLogo, 
                    E.hide_join_room            AS HideJoinRoom, 
                    E.open_registration_new_tab AS OpenRegistrationNewTab, 
                    E.schedule_app              AS ScheduleApp, 
                    E.schedule_description      AS ScheduleDescription, 
                    E.venue_map                 AS VenueMap, 
                    E.code_of_conduct           AS CodeOfConduct, 
                    E.sponsor_benefits          AS SponsorBenefits, 
                    E.sponsor_menuitem          AS SponsorMenuItem, 
                    E.precon_description        AS PreconDescription
                FROM Events AS E
                WHERE E.event_id = @eventId
                ORDER BY E.event_date DESC
                """;

            Event evt = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                evt = await connection.QueryFirstOrDefaultAsync<Event>(query, new { eventId });

                query =
                    """
                    SELECT 
                        P.precon_id         AS Id,
                        P.name              AS Name,
                        P.description       AS Description,
                        P.registration_url  AS RegistrationUrl,
                        --------------------------------------
                        S.speaker_order,    -- SPLIT
                        S.speaker_name AS Name, 
                        S.speaker_url AS Profile, 
                        S.speaker_img AS [Image]
                    FROM Precons AS P
                    OUTER APPLY (
                        SELECT *
                        FROM (
                            VALUES 
                                (1, P.speaker1_name, P.speaker1_url, P.speaker1_img),
                                (2, P.speaker2_name, P.speaker2_url, P.speaker2_img),
                                (3, P.speaker3_name, P.speaker3_url, P.speaker3_img),
                                (4, P.speaker4_name, P.speaker4_url, P.speaker4_img)
                        ) AS S (speaker_order, speaker_name, speaker_url, speaker_img)
                        WHERE COALESCE(speaker_name, speaker_url, speaker_img) IS NOT NULL
                    ) AS S
                    WHERE P.event_id = @eventId
                    """;


                var precons = await connection.QueryAsync<Precon, Speaker, Precon>(query, (p, s) => { p.Speakers.Add(s); return p; }, new { eventId }, splitOn: "speaker_order");
                evt.Precons.AddRange(precons);

                query =
                    """
                    SELECT 
                        R.room_id  AS Id, 
                        R.name     AS Name,
                        R.URL      AS JoinURL
                    FROM Rooms AS R
                    WHERE R.event_id = @eventId
                    """;
                var rooms = await connection.QueryAsync<Room>(query, new { eventId });
                evt.Rooms.AddRange(rooms);

                query =
                    """
                    SELECT 
                        M.milestone_id        AS Id,
                        M.[order]             AS [Order],
                        M.name                AS Name,
                        M.milestone_date      AS [Date],
                        M.milestone_date_text AS [DateText]
                    FROM Milestones AS M
                    WHERE M.event_id = @eventId
                    """;

                var milestones = await connection.QueryAsync<Milestone>(query, new { eventId });
                evt.Milestones.AddRange(milestones);

                query =
                    """"
                    SELECT 
                        O.organizer_id      AS Id,
                        O.name              AS Name,
                        O.email             AS Email,
                        O.twitter           AS Twitter
                    FROM Organizers AS O
                    WHERE O.event_id = @eventId
                    """";

                var organizers = await connection.QueryAsync<Organizer>(query, new { eventId });
                evt.Organizers.AddRange(organizers);

                query =
                    """"
                    SELECT 
                    	 S.sponsor_id AS Id
                    	,S.sponsor_level AS [Level]
                    	,S.image_url AS [ImageURL]
                    	,S.image_bin AS [Image]
                    	,S.link_url AS [Link]
                    	,S.height_px AS [Height]
                    	,S.width_px AS [Width]
                    	,S.margin_top_px [Top]
                    	,S.margin_bottom_px [Bottom]
                    FROM Sponsors AS S
                    WHERE S.event_id = @eventId
                    """";

                var sponsors = await connection.QueryAsync<Sponsor>(query, new { eventId });
                evt.Sponsors.AddRange(sponsors);
            }
            return evt;
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
                    E.sponsor_menuitem AS SponsorMenuItem
                FROM Events AS E
                ORDER BY E.event_date DESC
            ";

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Event>(query);
        }
    }
}
