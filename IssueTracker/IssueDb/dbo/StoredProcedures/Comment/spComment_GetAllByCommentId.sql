CREATE PROCEDURE [dbo].[spComment_GetAllByCommentId]
	@CommentId INT
AS
begin
	select *
    into #comm_temp
	from dbo.[Comment]
	where [Comment].IsDeleted = 0 /*FALSE*/ AND [Comment].CommentId = @CommentId
	order by [Comment].Updated desc;

    SELECT *
    INTO #file
	FROM dbo.[File]
	WHERE [File].FileCommentId in (select Id from #comm_temp) AND [File].FileIsDeleted = 0

    SELECT *
    FROM #comm_temp

    SELECT *
    FROM #file

end
