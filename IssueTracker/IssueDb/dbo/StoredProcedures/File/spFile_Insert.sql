CREATE PROCEDURE [dbo].[spFile_Insert]
	@FileId VARCHAR(36),
    @Extension NVARCHAR(350),
    @FileIssueId INT,
    @FileCommentId INT
AS
	insert into dbo.[File] (FileId, Extension, FileIssueId, FileCommentId)
	values (@FileId, @Extension, @FileIssueId, @FileCommentId)
    select FileId
    from dbo.[File]
    where @FileId = FileId
RETURN 0
