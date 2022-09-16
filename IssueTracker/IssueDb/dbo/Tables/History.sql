CREATE TABLE [dbo].[History]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Type] INT NOT NULL,
    [ProjectId] INT NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [IssueId] INT,
    [ReferenceType] INT,
    [ReferenceId] INT,
    [Field] VARCHAR(1024),
    [OldValue] VARCHAR (max),
    [NewValue] VARCHAR (max),
    [Updated] [datetime2](7) NOT NULL,
    FOREIGN KEY (ProjectId) REFERENCES [dbo].[Project](Id),
    FOREIGN KEY (IssueId) REFERENCES [dbo].[Issue](Id),
    FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id),

)
