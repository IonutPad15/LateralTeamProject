CREATE TABLE [dbo].[TimeTracker]
(
	[Id] INT PRIMARY KEY Identity,
    [Name] varchar(20) not null,
    [Description] varchar(max),
    [Date] datetime not null,
    [Worked] smallint not null,
    [Billable] smallint not null,
    [Remaining] smallint not null,
    [IssueId] int not null,
    [UserId] UNIQUEIDENTIFIER not null,
    [IsDeleted] bit,
    FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id),
	FOREIGN KEY (IssueId) REFERENCES [dbo].[IssueType](Id)
)
