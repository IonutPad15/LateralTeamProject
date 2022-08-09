CREATE PROCEDURE [dbo].[spComment_Get]
	@Id INT
AS
begin
	select Id, UserId, IssueId, Body, Created, Updated, IsDeleted
	from dbo.[Comment] 
	where IsDeleted = 0;
end	