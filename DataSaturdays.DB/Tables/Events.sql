CREATE TABLE [dbo].[Events]
(
	[event_id] uniqueidentifier NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(128) NOT NULL, 
    [organization_id] uniqueidentifier,
    [event_date] DATE NOT NULL, 
    [description] NVARCHAR(MAX) NOT NULL, 
    [registration_url] VARCHAR(2000) NULL, 
    [schedule_url] VARCHAR(2000) NULL, 
    [speaker_list_url] VARCHAR(2000) NULL, 
    [volunteer_url] VARCHAR(2000) NULL, 
    [hide_top_logo] BIT NULL, 
    [hide_join_room] BIT NULL, 
    [open_registration_new_tab] BIT NULL, 
    [precon_description] NVARCHAR(MAX) NULL, 
    [schedule_app] NCHAR(10) NULL, 
    [venue_map] NCHAR(10) NULL, 
    [code_of_conduct] NVARCHAR(MAX) NULL,

)
