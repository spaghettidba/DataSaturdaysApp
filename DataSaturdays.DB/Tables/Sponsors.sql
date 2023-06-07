CREATE TABLE [dbo].[Sponsors]
(
	[sponsor_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [event_id] UNIQUEIDENTIFIER NOT NULL,
    [sponsor_level] varchar(20),
    [image_url] VARCHAR(2000) NULL, 
    [image_bin] VARBINARY(MAX) NULL, 
    [link_url] VARCHAR(2000) NULL, 
    [width_px] INT NULL,
    [height_px] INT NULL,
    [margin_top_px] INT NULL,
    [margin_bottom_px] INT NULL,
    CONSTRAINT FK_Sponsors_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)	
)
