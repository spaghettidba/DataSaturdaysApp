CREATE TABLE [dbo].[OrganizationTokens]
(
	[token_id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [organization_id] INT NOT NULL 
		CONSTRAINT FK_organizationTokens_organizations_o
			FOREIGN KEY (organization_id) 
			REFERENCES Organizations, 
    [creation_date_utc] DATETIME2 NOT NULL DEFAULT GETUTCDATE()
)
