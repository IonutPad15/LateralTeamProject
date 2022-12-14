CREATE TABLE [dbo].[History]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Type] INT NOT NULL,
    [ProjectId] INT NOT NULL,
    [Author] NVARCHAR (30) NOT NULL,
    [IssueId] INT,
    [ReferenceType] INT,
    [ReferenceId] VARCHAR(36),
    [Field] VARCHAR(1024),
    [OldValue] VARCHAR (max),
    [NewValue] VARCHAR (max),
    [Updated] [datetime2](7) NOT NULL,
    FOREIGN KEY (ProjectId) REFERENCES [dbo].[Project](Id),
    FOREIGN KEY (IssueId) REFERENCES [dbo].[Issue](Id)

)
