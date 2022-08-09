CREATE PROCEDURE [dbo].[spIssue_Get]
	@Id INT
AS
begin
	select Id, Title, Description, Created, Updated, ProjectId, UserAssignedId, PriorityId, StatusId, RoleId, IsDeleted
	from dbo.[Issue]
	where IsDeleted = 0;
end