CREATE TABLE [dbo].[Users]
(
	[user_id] UNIQUEIDENTIFIER NOT NULL 
		CONSTRAINT PK_Users PRIMARY KEY,
	[email] nvarchar(128) NOT NULL UNIQUE,
	[aspnet_id] nvarchar(450) NOT NULL UNIQUE
		CONSTRAINT FK_aspnet_user FOREIGN KEY REFERENCES AspNetUsers (Id),
	[current_organization] int NULL
		CONSTRAINT FK_defaultorg FOREIGN KEY REFERENCES Organizations (organization_id), 
    [twitter_url] VARCHAR(2000) NULL 
)
