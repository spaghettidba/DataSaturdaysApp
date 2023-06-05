CREATE TABLE [dbo].[Sponsors]
(
	[sponsor_id] INT NOT NULL PRIMARY KEY, 
    [event_id] UNIQUEIDENTIFIER NOT NULL,
    [name] NVARCHAR(128) NOT NULL, 
    [image_url] VARCHAR(2000) NULL, 
    [image_bin] VARBINARY(MAX) NULL, 
    [link_url] VARCHAR(2000) NULL, 
    [height_px] INT NULL,
    [margin_top_px] INT NULL,
    [margin_bottom_px] INT NULL,
    CONSTRAINT FK_Sponsors_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)	
)
