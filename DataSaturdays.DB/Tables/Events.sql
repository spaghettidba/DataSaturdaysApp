CREATE TABLE [dbo].[Events]
(
	[event_id] uniqueidentifier NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(128) NOT NULL, 
    [event_date] DATE NOT NULL, 
    [virtual] bit NOT NULL,
    [description] NVARCHAR(MAX) NOT NULL, 
    [registration_url] VARCHAR(2000) NULL,
    [callforspeakers_url] VARCHAR(2000) NULL, 
    [schedule_url] VARCHAR(2000) NULL, 
    [speaker_list_url] VARCHAR(2000) NULL, 
    [volunteer_url] VARCHAR(2000) NULL, 
    [hide_top_logo] BIT NULL, 
    [hide_join_room] BIT NULL, 
    [open_registration_new_tab] BIT NULL, 
    [schedule_app] VARCHAR(2000) NULL, 
    [schedule_description] NVARCHAR(MAX),
    [venue_map] VARCHAR(2000) NULL, 
    [code_of_conduct] NVARCHAR(MAX) NULL, 
    [sponsor_benefits] NVARCHAR(MAX) NULL, 
    [sponsor_menuitem] BIT NULL, 
    [precon_description] nvarchar(max) NULL, 
    [published] BIT NULL
)
