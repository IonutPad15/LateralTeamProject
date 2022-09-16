CREATE PROCEDURE [dbo].[spFile_Insert]
	@FileId VARCHAR(36),
    @Extension NVARCHAR(350),
    @FileIssueId INT,
    @FileCommentId INT,
    @Updated DATETIME,
    @FileUserId UNIQUEIDENTIFIER
AS
	insert into dbo.[File] (FileId, Extension, FileIssueId, FileCommentId, Updated, FileUserId)
	values (@FileId, @Extension, @FileIssueId, @FileCommentId, @Updated, @FileUserId)
    select FileId
    from dbo.[File]
    where @FileId = FileId
RETURN 0
