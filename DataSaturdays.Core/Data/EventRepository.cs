using Dapper;
using DataSaturdays.Core.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        private const string _base_query = """
            SELECT 
                E.event_id                  AS Id,
                E.name                      AS Name,
                E.event_date                AS [Date],
                E.virtual                   AS Virtual,
                E.description               AS Description,
                E.registration_url          AS RegistrationURL,
                E.callforspeakers_url       AS CallForSpeakersURL,
                E.schedule_url              AS ScheduleURL,
                E.speaker_list_url          AS SpeakerListURL, 
                E.volunteer_url             AS VolunteerRequestrURL, 
                E.hide_top_logo             AS HideTopLogo, 
                E.hide_join_room            AS HideJoinRoom, 
                E.open_registration_new_tab AS OpenRegistrationNewTab, 
                E.schedule_app              AS ScheduleApp, 
                E.venue_map                 AS VenueMap, 
                E.code_of_conduct           AS CodeOfConduct, 
                E.sponsor_benefits          AS SponsorBenefits, 
                E.sponsor_menuitem          AS SponsorMenuItem,
                E.published                 AS Published
            FROM Events AS E
            """ + "\r\n";


        public EventRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        } 

        public async Task<Guid> CreateEvent(Event Input)
        {
            try {
                Input.Id = Guid.NewGuid();
                Input.Published = false;

                string sql = """"
                INSERT INTO Events
                VALUES (
                    @Id,
                    @Name,
                    @Date,
                    @Virtual,
                    @Description,
                    @RegistrationURL,
                    @CallForSpeakersURL,
                    @ScheduleURL,
                    @SpeakerListURL,
                    @VolunteerRequestURL,
                    @HideTopLogo,
                    @HideJoinRoom,
                    @OpenRegistrationNewTab,
                    @ScheduleApp,
                    @ScheduleDescription,
                    @VenueMap,
                    @CodeOfConduct,
                    @SponsorBenefits,
                    @SponsorMenuItem,
                    @PreconDescription,
                    @Published
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
                return Input.Id;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Guid.Empty;
            }
        }

        public async Task DeleteEvent(Event Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Organizers
                WHERE event_id = @Id
                DELETE FROM Milestones
                WHERE event_id = @Id
                DELETE FROM Precons
                WHERE event_id = @Id
                DELETE FROM Sponsors
                WHERE event_id = @Id
                DELETE FROM Rooms
                WHERE event_id = @Id
                DELETE FROM Events
                WHERE event_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task PublishEvent(Event Input)
        {
            try
            {
                string sql = """"
                UPDATE Events
                SET published = @Published
                WHERE event_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            string query = _base_query +
                """
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
                        P.registration_url  AS RegistrationUrl

                    FROM Precons AS P
                    WHERE P.event_id = @eventId
                    
                    """;

                if (evt == null)
                {
                    return null;
                }

                var precons = await connection.QueryAsync<Preconference>(query, new { eventId });

                evt.Precons?.AddRange(precons);

                query =
                    """
                    SELECT 
                        S.speaker_id         AS Id,
                        S.precon_id          AS PreconId,
                        S.name               AS Name,
                        S.speaker_url        AS Profile,
                        S.speaker_image_url  AS Image

                    FROM Speakers AS S
                    WHERE S.precon_id = @Id
                    
                    """;

                foreach (var Pre in evt.Precons)
                {
                    var speakers = await connection.QueryAsync<Speaker>(query, new { Pre.Id });
                    Pre.Speakers.AddRange(speakers);
;                }

                query =
                    """
                    SELECT 
                        R.room_id  AS Id, 
                        R.name     AS Name,
                        R.URL      AS JoinURL
                    FROM Rooms AS R
                    WHERE R.event_id = @eventId
                    ORDER BY R.name
                    """;
                var rooms = await connection.QueryAsync<Room>(query, new { eventId });
                evt.Rooms?.AddRange(rooms);

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
                    ORDER BY M.[order]
                    """;

                var milestones = await connection.QueryAsync<Milestone>(query, new { eventId });
                evt.Milestones?.AddRange(milestones);

                query =
                    """"
                    SELECT 
                        O.organizer_id      AS Id,
                        O.name              AS Name,
                        O.email             AS Email,
                        O.twitter           AS Twitter
                    FROM Organizers AS O
                    WHERE O.event_id = @eventId
                    ORDER BY O.name
                    """";

                var organizers = await connection.QueryAsync<Organizer>(query, new { eventId });
                evt.Organizers?.AddRange(organizers);

                query =
                    """"
                    SELECT 
                    	 S.sponsor_id AS Id
                    	,S.sponsor_level AS [Level]
                    	,S.image_url AS [ImageURL]
                    	,S.image_bin AS [Image]
                    	,S.link_url AS [LinkURL]
                    	,S.height_px AS [Height]
                    	,S.width_px AS [Width]
                    	,S.margin_top_px [Top]
                    	,S.margin_bottom_px [Bottom]
                        ,S.name AS [Name]
                    FROM Sponsors AS S
                    WHERE S.event_id = @eventId
                    """";

                var sponsors = await connection.QueryAsync<Sponsor>(query, new { eventId });
                evt.Sponsors?.AddRange(sponsors);
            }
            return evt;
        }

        public async Task<IEnumerable<Event>> GetEvents()
        {
            string query = _base_query +
            """
                ORDER BY E.event_date DESC
            """;

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Event>(query);
        }

        public async Task<IEnumerable<Event>> GetEventsByUser(Guid userId)
        {
            const string query = _base_query +
                """
                WHERE EXISTS (
                    SELECT *
                    FROM Organizers AS O
                    INNER JOIN AspNetUsers AS U
                        ON O.email = U.Email
                    WHERE O.event_id = E.event_id
                        AND U.Id = @userId
                )
                ORDER BY E.event_date DESC
            """;

            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Event>(query, new { userId });
        }

        public async Task UpdateEvent(Event Input)
        {
            try
            {
                string sql = """"
                UPDATE Events 
                SET name                        = @Name,
                    event_date                  = @Date, 
                    virtual                     = @Virtual, 
                    description                 = @Description, 
                    registration_url            = @RegistrationURL,
                    callforspeakers_url         = @CallForSpeakersURL, 
                    schedule_url                = @ScheduleURL, 
                    speaker_list_url            = @SpeakerListURL, 
                    volunteer_url               = @VolunteerRequestURL, 
                    hide_top_logo               = @HideTopLogo, 
                    hide_join_room              = @HideJoinRoom, 
                    open_registration_new_tab   = @OpenRegistrationNewTab, 
                    schedule_app                = @ScheduleApp, 
                    schedule_description        = @ScheduleDescription, 
                    venue_map                   = @VenueMap, 
                    code_of_conduct             = @CodeOfConduct,
                    sponsor_benefits            = @SponsorBenefits, 
                    sponsor_menuitem            = @SponsorMenuItem, 
                    precon_description          = @PreconDescription, 
                    published                   = @Published
                WHERE event_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task UpdateMilestone(Milestone Input)
        {
            try
            {
                string sql = """"
                UPDATE Milestones 
                SET name = @Name,
                    milestone_date = @Date,
                    [order] = @Order
                WHERE milestone_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CreateMilestone(Milestone Input)
        {
            try
            {
                Input.Id = Guid.NewGuid();

                string sql = """"
                INSERT INTO Milestones 
                VALUES(
                    @Id,
                    @EventId,
                    @Order,
                    @Name,
                    @Date,
                    @DateText
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task UpdateOrganizer(Organizer Input)
        {
            try
            {
                string sql = """"
                UPDATE Organizers 
                SET name = @Name,
                    email = @Email,
                    twitter = @Twitter
                WHERE organizer_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CreateOrganizer(Organizer Input)
        {
            try
            {
                Input.Id = Guid.NewGuid();

                string sql = """"
                INSERT INTO Organizers
                VALUES(
                    @Id,
                    @EventId,
                    @Name,
                    @Email,
                    @Twitter
                )
                """";
                
                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task UpdateSponsor(Sponsor Input)
        {
            try
            {
                string sql = """"
                UPDATE Sponsors
                SET name = @Name,
                    sponsor_level = @Level,
                    image_url = @ImageURL,
                    image_bin = @Image,
                    link_url = @LinkURL,
                    width_px = @Width,
                    height_px = @Height,
                    margin_top_px = @MarginTop,
                    margin_bottom_px = @MarginBottom
                WHERE sponsor_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CreateSponsor(Sponsor Input)
        {
            try
            {
                Input.Id = Guid.NewGuid();

                string sql = """"
                INSERT INTO Sponsors 
                VALUES(
                    @Id,
                    @EventId,
                    @Level,
                    @ImageURL,
                    @Image,
                    @LinkURL,
                    @Width,
                    @Height,
                    @MarginTop,
                    @MarginBottom,
                    @Name
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task UpdateRoom(Room Input)
        {
            try
            {
                string sql = """"
                UPDATE Rooms 
                SET name = @Name,
                    URL = @JoinURL
                WHERE room_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CreateRoom(Room Input)
        {
            try
            {
                Input.Id = Guid.NewGuid();

                string sql = """"
                INSERT INTO Rooms
                VALUES(
                    @Id,
                    @EventId,
                    @Name,
                    @JoinURL
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
        public async Task UpdatePrecon(Preconference Input)
        {
            try
            {
                string sql = """"
                UPDATE Precons
                SET name = @Name,
                    description = @Description,
                    registration_url = @RegistrationUrl
                WHERE precon_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CreatePrecon(Preconference Input)
        {
            try
            {
                Input.Id = Guid.NewGuid();

                string sql = """"
                INSERT INTO Precons
                VALUES(
                    @Id,
                    @EventId,
                    @Name,
                    @Description,
                    @RegistrationUrl
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }       
        
        public async Task UpdateSpeaker(Speaker Input)
        {
            try
            {
                string sql = """"
                UPDATE Speakers
                SET name = @Name,
                    speaker_url = @Profile,
                    speaker_image_url = @Image
                WHERE speaker_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public async Task CreateSpeaker(Speaker Input)
        {
            try
            {
                Input.Id = Guid.NewGuid();

                string sql = """"
                INSERT INTO Speakers
                VALUES(
                    @Id,
                    @PreconId,
                    @Name,
                    @Profile,
                    @Image
                )
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteMilestone(Milestone Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Milestones
                WHERE milestone_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteOrganizer(Organizer Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Organizers
                WHERE organizer_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteRoom(Room Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Rooms
                WHERE room_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }  
        
        public async Task DeleteSponsor(Sponsor Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Sponsors
                WHERE sponsor_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
        
        public async Task DeletePrecon(Preconference Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Precons
                WHERE precon_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }        
        
        public async Task DeleteSpeaker(Speaker Input)
        {
            try
            {
                string sql = """"
                DELETE FROM Speakers
                WHERE speaker_id = @Id
                """";

                using var connection = new SqlConnection(_connectionString);
                await connection.ExecuteAsync(sql, Input);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<Event>> GetEventsByUserNameAsync(string UserName)
        {
            try
            {
                string query = _base_query +
                """
                JOIN Organizers ON E.event_id = Organizers.event_id
                WHERE Organizers.email = @UserName
                ORDER BY E.event_date DESC
                """;

                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryAsync<Event>(query, new { UserName });
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Event>();
            }
        }        
        
        public async Task<IEnumerable<Event>> GetEventsByAdmin()
        {
            try
            {
                string query =
                """
                SELECT [event_id] AS [Id]
                ,[name] AS [Name]
                ,[event_date] AS [Date]
                ,[virtual] AS [Virtual]
                ,[description] AS [Description]
                ,[registration_url] AS [RegistrationURL]
                ,[callforspeakers_url] AS [CallForSpeakersURL]
                ,[schedule_url] AS [ScheduleURL]
                ,[speaker_list_url] AS [SpeakerListURL]
                ,[volunteer_url] AS [VolunteerRequestURL]
                ,[hide_top_logo] AS [HideTopLogo]
                ,[hide_join_room] AS [HideJoinRoom]
                ,[open_registration_new_tab] AS [OpenRegistrationNewTab]
                ,[schedule_app] AS [ScheduleApp]
                ,[schedule_description] AS [ScheduleDescription]
                ,[venue_map] AS [VenueMap]
                ,[code_of_conduct] AS [CodeOfConduct]
                ,[sponsor_benefits] AS [SponsorBenefits]
                ,[sponsor_menuitem] AS [SponsorMenuItem]
                ,[precon_description] AS [PreconDescription]
                ,[published] AS [Published]
                FROM dbo.Events
                ORDER BY [Date] DESC
                """;

                using var connection = new SqlConnection(_connectionString);
                return await connection.QueryAsync<Event>(query);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Event>();
            }
        }
    }
}
