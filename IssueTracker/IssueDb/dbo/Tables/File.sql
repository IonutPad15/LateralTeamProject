CREATE TABLE [dbo].[File]
(
	[FileId] VARCHAR(36) NOT NULL PRIMARY KEY,
    [Extension] NVARCHAR(350) NOT NULL,
    [FileIssueId] INT NOT NULL,
    [FileCommentId] INT,
    [FileIsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Updated] DATETIME NOT NULL,
    [FileUserId] UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY (FileIssueId) REFERENCES [dbo].[Issue](Id),
	FOREIGN KEY (FileCommentId) REFERENCES [dbo].[Comment](Id),
    FOREIGN KEY (FileUserId) REFERENCES [dbo].[User](Id)
)
