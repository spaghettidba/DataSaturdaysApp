CREATE TABLE [dbo].[Organizers]
(
	[organizer_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [user_id] UNIQUEIDENTIFIER NOT NULL,
	[event_id] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT FK_Organizers_Users FOREIGN KEY (user_id) REFERENCES Users (user_id),
	CONSTRAINT FK_Organizers_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)
)
