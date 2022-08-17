CREATE PROCEDURE [dbo].[spComment_GetAllByIssueId]
	@IssueId INT
AS
begin
	select * 
	into #comm_temp
	from dbo.[Comment]
	where [Comment].IsDeleted = 0 AND [Comment].IssueId = @IssueId
	order by [Comment].Updated desc
	SET IDENTITY_INSERT #comm_temp ON;
	insert into #comm_temp (Id,UserId, IssueId, CommentId, Author, Body, Created, Updated, IsDeleted)
	(
		select Id,UserId, IssueId, CommentId, Author, Body, Created, Updated, IsDeleted
		from dbo.[Comment]
		where [Comment].IsDeleted = 0 AND [Comment].CommentId in 
			(select Id from #comm_temp)
		)
	
	select * 
	from #comm_temp
	order by #comm_temp.Updated desc
 
end