CREATE PROCEDURE [dbo].[spComment_GetAllByIssueId]
	@IssueId INT
AS
begin
	select * 
	into #comm_temp
	from dbo.[Comment]
	where [Comment].IsDeleted = 0 AND [Comment].IssueId = @IssueId
	SET IDENTITY_INSERT #comm_temp ON;
	insert into #comm_temp (Id,UserId, IssueId, CommentId, Author, Body, Created, Updated, IsDeleted)
	(
		select Id,UserId, IssueId, CommentId, Author, Body, Created, Updated, IsDeleted
		from dbo.[Comment]
		where [Comment].IsDeleted = 0 AND [Comment].CommentId in 
			(select Id from #comm_temp)
		)

    SELECT *
    INTO #file
	FROM dbo.[File]
	WHERE [File].FileCommentId in (select Id from #comm_temp) AND [File].FileIsDeleted = 0

    SELECT *
    FROM #comm_temp

    SELECT *
    FROM #file
end
