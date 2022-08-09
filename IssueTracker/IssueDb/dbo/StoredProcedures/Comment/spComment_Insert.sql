CREATE PROCEDURE [dbo].[spComment_Insert]
	@UserId UNIQUEIDENTIFIER,
	@IssueId INT, 
	@Body NVARCHAR(max),
	@Created datetime2(7), 
	@Updated datetime2(7)
AS
begin
	insert into dbo.[Comment](UserId,IssueId, Body, Created, Updated)
	values (@UserId, @IssueId, @Body, @Created, @Updated);
end