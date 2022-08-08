CREATE TABLE [dbo].[Issue]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[ProjectId] INT NOT NULL,
	[IssueTypeId] INT NOT NULL,
	[UserAssignedId] UNIQUEIDENTIFIER NOT NULL, 
	[PriorityId] INT NOT NULL,  
	[StatusId] INT NOT NULL, 
	[RoleId] INT NOT NULL,
	[IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
	FOREIGN KEY (UserAssignedId) REFERENCES [dbo].[User](Id),
	FOREIGN KEY (IssueTypeId) REFERENCES [dbo].[IssueType](Id),
	FOREIGN KEY (PriorityId) REFERENCES [dbo].[Priority](Id),
	FOREIGN KEY (StatusId) REFERENCES [dbo].[Status](Id),
	FOREIGN KEY (ProjectId) REFERENCES [dbo].[Project](Id),
	FOREIGN KEY (RoleId) REFERENCES [dbo].[Role](Id)
)
