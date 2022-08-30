CREATE PROCEDURE [dbo].[spFile_Insert]
	@FileId VARCHAR(36),
    @IssueId INT,
    @CommentId INT
AS
	insert into dbo.[File] (FileId, IssueId, CommentId)
	values (@FileId, @IssueId, @CommentId)
    select CAST(SCOPE_IDENTITY() as int);
RETURN 0
