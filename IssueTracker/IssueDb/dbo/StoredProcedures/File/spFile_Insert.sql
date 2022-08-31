CREATE PROCEDURE [dbo].[spFile_Insert]
	@FileId VARCHAR(36),
    @GroupId NVARCHAR(350),
    @FileIssueId INT,
    @FileCommentId INT
AS
	insert into dbo.[File] (FileId,GroupId, FileIssueId, FileCommentId)
	values (@FileId, @GroupId, @FileIssueId, @FileCommentId)
    select FileId
    from dbo.[File]
    where @FileId = FileId
RETURN 0
