CREATE TABLE [dbo].[Milestones]
(
	[milestone_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [event_id] UNIQUEIDENTIFIER NOT NULL,
    [order] smallint NOT NULL,
    [name] NVARCHAR(128) NOT NULL, 
    [milestone_date] date NULL,
    [milestone_date_text] nvarchar(100) NULL,
    CONSTRAINT CK_date_exclusive CHECK (NOT([milestone_date] IS NOT NULL AND [milestone_date_text] IS NOT NULL)),
    CONSTRAINT FK_Milestones_Events FOREIGN KEY (event_id) REFERENCES Events (event_id)
)
