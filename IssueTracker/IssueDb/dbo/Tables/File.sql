CREATE TABLE [dbo].[File]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[FileId] VARCHAR(36) NOT NULL,
    [IssueId] INT,
    [CommentId] INT,
    [IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
    FOREIGN KEY (IssueId) REFERENCES [dbo].[Issue](Id),
	FOREIGN KEY (CommentId) REFERENCES [dbo].[Comment](Id)
)
