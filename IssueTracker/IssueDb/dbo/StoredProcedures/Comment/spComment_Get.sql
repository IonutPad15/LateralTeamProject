CREATE PROCEDURE [dbo].[spComment_Get]
	@Id INT
AS
begin
	select *
	from dbo.[Comment]
    left join [File] on [File].FileCommentId = @Id AND [File].FileIsDeleted = 0
	where [Comment].IsDeleted = 0/*FALSE*/ AND [Comment].Id = @Id; 
end	
