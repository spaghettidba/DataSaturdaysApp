CREATE TABLE [dbo].[Organizations] (
    [organization_id]        INT            NOT NULL,
    [name]                   NVARCHAR (128) NOT NULL,
    [display_name]           NVARCHAR (128) NULL,
    CONSTRAINT PK_Organizations PRIMARY KEY CLUSTERED ([organization_id] ASC),
    CONSTRAINT [UQ_name] UNIQUE NONCLUSTERED ([name] ASC)
);


