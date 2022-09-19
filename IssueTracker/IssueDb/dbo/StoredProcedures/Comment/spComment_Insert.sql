CREATE PROCEDURE [dbo].[spComment_Insert]
	@UserId UNIQUEIDENTIFIER,
	@IssueId INT,
	@CommentId INT,
	@Author NVARCHAR(30),
	@Body NVARCHAR(450),
	@Created datetime2(7), 
	@Updated datetime2(7)
AS
begin
	insert into dbo.[Comment] (UserId, IssueId, CommentId, Author, Body, Created, Updated)
	values (@UserId, @IssueId, @CommentId,@Author, @Body, @Created, @Updated);
    select CAST(SCOPE_IDENTITY() as int);
end
