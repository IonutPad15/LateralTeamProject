CREATE PROCEDURE [dbo].[spComment_GetAllByCommentId]
	@CommentId INT
AS
begin
	select * 
	from dbo.[Comment]
	where [Comment].IsDeleted = 0 AND [Comment].CommentId = @CommentId
	order by [Comment].Updated desc;
end
