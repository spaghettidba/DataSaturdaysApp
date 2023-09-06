CREATE TABLE [dbo].[Precons]
(
    [precon_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [event_id] UNIQUEIDENTIFIER NOT NULL,
    [name] NVARCHAR(128) NOT NULL, 
    [description] NVARCHAR(MAX) NULL,
    [registration_url] VARCHAR(2000) NULL,
    CONSTRAINT FK_Precons_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)
)
