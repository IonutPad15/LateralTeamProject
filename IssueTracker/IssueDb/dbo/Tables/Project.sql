CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] NVARCHAR(50) NOT NULL, 
	[Description] NVARCHAR(max) NOT NULL,
	[Created] [datetime2](7) NOT NULL,
	[IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
)
