CREATE TABLE [dbo].[Issues]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] NVARCHAR(50) NOT NULL,
	[Body] NVARCHAR(max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[ProjectId] INT NOT NULL,
	[IssueTypeId] INT NOT NULL,
	[UserAssignedId] UNIQUEIDENTIFIER NOT NULL, 
	[PriorityId] INT NOT NULL,  
	[StatusId] INT NOT NULL, 
	[IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
	FOREIGN KEY (UserAssignedId) REFERENCES [dbo].[Users](Id),
	FOREIGN KEY (IssueTypeId) REFERENCES [dbo].[IssueTypes](Id),
	FOREIGN KEY (PriorityId) REFERENCES [dbo].[Priorities](Id),
	FOREIGN KEY (StatusId) REFERENCES [dbo].[Statuses](Id),
	FOREIGN KEY (ProjectId) REFERENCES [dbo].[Projects](Id)
)
