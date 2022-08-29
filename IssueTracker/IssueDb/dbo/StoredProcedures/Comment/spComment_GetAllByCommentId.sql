CREATE PROCEDURE [dbo].[spComment_GetAllByCommentId]
	@CommentId INT
AS
begin
    if exists(select * from Comment where Id = @CommentId)
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
    else
		THROW 5100, 'The record does not exist.', 1;
end
