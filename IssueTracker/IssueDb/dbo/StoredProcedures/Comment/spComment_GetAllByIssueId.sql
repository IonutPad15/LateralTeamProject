CREATE PROCEDURE [dbo].[spComment_GetAllByIssueId]
	@IssueId INT
AS
begin
	select * 
	from dbo.[Comment]
	where [Comment].IsDeleted = 0 AND [Comment].IssueId = @IssueId
	order by [Comment].Updated desc
end
