CREATE TABLE [dbo].[UserOrganizations]
(
	[user_id] UNIQUEIDENTIFIER NOT NULL 
		CONSTRAINT FK_users_organizations_u
		FOREIGN KEY (user_id) 
		REFERENCES Users,
	[organization_id] INT NOT NULL 
		CONSTRAINT FK_users_organizations_o
		FOREIGN KEY (organization_id) 
		REFERENCES Organizations
	CONSTRAINT PK_user_organizations PRIMARY KEY (user_id, organization_id), 
    [is_owner] BIT NOT NULL DEFAULT 0 
)
