CREATE TABLE [dbo].[File]
(
	[FileId] VARCHAR(36) NOT NULL PRIMARY KEY,
    [GroupId] NVARCHAR(350) NOT NULL,
    [FileIssueId] INT,
    [FileCommentId] INT,
    [FileIsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
    FOREIGN KEY (FileIssueId) REFERENCES [dbo].[Issue](Id),
	FOREIGN KEY (FileCommentId) REFERENCES [dbo].[Comment](Id)
)
