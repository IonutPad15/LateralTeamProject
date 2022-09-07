CREATE PROCEDURE [dbo].[spFile_Insert]
	@FileId VARCHAR(36),
    @Extension NVARCHAR(350),
    @FileIssueId INT,
    @FileCommentId INT,
    @Updated DATETIME
AS
	insert into dbo.[File] (FileId, Extension, FileIssueId, FileCommentId, Updated)
	values (@FileId, @Extension, @FileIssueId, @FileCommentId, @Updated)
    select FileId
    from dbo.[File]
    where @FileId = FileId
RETURN 0
