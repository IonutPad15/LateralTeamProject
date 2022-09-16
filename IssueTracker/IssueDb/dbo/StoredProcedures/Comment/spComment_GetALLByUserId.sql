CREATE PROCEDURE [dbo].[spComment_GetAllByUserId]
	@UserId UNIQUEIDENTIFIER
AS
begin
	SELECT *
    INTO #comm_temp
    FROM dbo.[Comment]
	WHERE [Comment].IsDeleted = 0 AND [Comment].UserId = @UserId
    ORDER BY [Comment].Updated desc

	SELECT *
    INTO #file
	FROM dbo.[File]
	WHERE [File].FileCommentId in (select Id from #comm_temp) AND [File].FileIsDeleted = 0

    SELECT *
    FROM #comm_temp

    SELECT *
    FROM #file
end
