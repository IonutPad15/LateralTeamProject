CREATE TABLE [dbo].[Comments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId] UNIQUEIDENTIFIER,
	[IssueId] INT NOT NULL,
	[Body] NVARCHAR(max) NOT NULL, 
	[Created] [datetime2](7) NOT NULL,
	[Updated] [datetime2](7) NOT NULL,
	[IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
	FOREIGN KEY (UserId) REFERENCES [dbo].[Users](Id),
	FOREIGN KEY (IssueId) REFERENCES [dbo].[Issues](Id)
)
