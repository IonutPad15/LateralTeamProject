CREATE PROCEDURE [dbo].[spIssue_GetAll]
AS
begin
	select *
	from Issue 
	inner join Project on Project.Id = ProjectId 
	inner join Status on Status.Id = StatusId
	inner join Priority on Priority.Id = PriorityId
	inner join Role on Role.Id = RoleId
	inner join IssueType on IssueType.Id = IssueTypeId
	inner join [User] on [User].Id = UserAssignedId
	where Issue.IsDeleted = 0

    select * from [dbo].[File];
end
