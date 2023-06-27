CREATE TABLE [dbo].[Organizers]
(
	[organizer_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
	[event_id] UNIQUEIDENTIFIER NOT NULL,
	[name] NVARCHAR(2000) NULL, 
    [email] VARCHAR(2000) NULL, 
    [twitter] VARCHAR(2000) NULL, 
	CONSTRAINT FK_Organizers_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)
)
