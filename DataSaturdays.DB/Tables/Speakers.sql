CREATE TABLE [dbo].[Speakers]
(
	[speaker_id] uniqueidentifier NOT NULL PRIMARY KEY,
	[precon_id] uniqueidentifier NOT NULL,
	[name] VARCHAR (128) NOT NULL,
	[speaker_url] VARCHAR (2000) NULL,
	[speaker_image_url] VARCHAR (2000) NULL,
	CONSTRAINT FK_Speakers_Precons FOREIGN KEY (precon_id) REFERENCES Precons (precon_id)
)
