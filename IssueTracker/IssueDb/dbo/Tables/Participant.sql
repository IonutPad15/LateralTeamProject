CREATE TABLE [dbo].[Participant]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] UNIQUEIDENTIFIER NOT NULL,
	[ProjectId] INT NOT NULL,
	[IssueId] INT NOT NULL,
	[RoleId] INT NOT NULL,
	[IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
	FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id),
	FOREIGN KEY (IssueId) REFERENCES [dbo].[Issue](Id),
	FOREIGN KEY (ProjectId) REFERENCES [dbo].[Project](Id),
	FOREIGN KEY (RoleId) REFERENCES [dbo].[Role](Id)
)
