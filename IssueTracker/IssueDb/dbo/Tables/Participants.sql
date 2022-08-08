CREATE TABLE [dbo].[Participants]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[ProjectId] INT NOT NULL,
	[IssueId] INT NOT NULL,
	FOREIGN KEY (UserId) REFERENCES [dbo].[Users](Id),
	FOREIGN KEY (IssueId) REFERENCES [dbo].[Issues](Id),
	FOREIGN KEY (ProjectId) REFERENCES [dbo].[Projects](Id)
)
