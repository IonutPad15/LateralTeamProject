CREATE PROCEDURE [dbo].[spComment_Get]
	@Id INT
AS
begin
    if exists(select * from Comment where Id = @Id)
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
    else
		THROW 5100, 'The record does not exist.', 1;
end	
