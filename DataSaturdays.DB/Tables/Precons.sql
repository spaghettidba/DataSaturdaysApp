CREATE TABLE [dbo].[Precons]
(
    [precon_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [event_id] UNIQUEIDENTIFIER NOT NULL,
    [name] NVARCHAR(128) NOT NULL, 
    [speaker1_name] NVARCHAR(128) NOT NULL, 
    [speaker1_url] VARCHAR(2000) NOT NULL, 
    [speaker1_img] VARCHAR(2000) NOT NULL, 
    [speaker2_name] NVARCHAR(128) NULL, 
    [speaker2_url] VARCHAR(2000) NULL, 
    [speaker2_img] VARCHAR(2000) NULL, 
    [speaker3_name] NVARCHAR(128) NULL, 
    [speaker3_url] VARCHAR(2000) NULL, 
    [speaker3_img] VARCHAR(2000) NULL, 
    [speaker4_name] NVARCHAR(128) NULL, 
    [speaker4_url] VARCHAR(2000) NULL, 
    [speaker4_img] VARCHAR(2000) NULL, 
    [description] NVARCHAR(MAX) NULL,
    [registration_url] VARCHAR(2000) NULL,
    CONSTRAINT FK_Precons_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)
)
