CREATE PROCEDURE [dbo].[spComment_Get]
	@Id INT
AS
begin
	select *
    into #comm_temp
	from dbo.[Comment]
	where [Comment].IsDeleted = 0/*FALSE*/ AND [Comment].Id = @Id;

    SELECT *
    INTO #file
	FROM dbo.[File]
	WHERE [File].FileCommentId = @Id AND [File].FileIsDeleted = 0

    SELECT *
    FROM #comm_temp

    SELECT *
    FROM #file
end	
