CREATE PROCEDURE [dbo].[spComment_GetAllByUserId]
	@UserId UNIQUEIDENTIFIER
AS
begin
	select * 
	from dbo.[Comment]
	where [Comment].IsDeleted = 0 AND [Comment].UserId = @UserId
	order by [Comment].Updated desc;
end
