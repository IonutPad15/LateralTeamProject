CREATE PROCEDURE [dbo].[spIssue_Get]
	@Id INT
AS
begin
	select *
	from dbo.[Issue]
	left join Project on Project.Id = ProjectId 
	left join Status on Status.Id = StatusId
	left join Priority on Priority.Id = PriorityId
	left join Role on Role.Id = RoleId
	left join IssueType on IssueType.Id = IssueTypeId
	left join [User] on [User].Id = UserAssignedId
	where Issue.IsDeleted = 0 and Issue.Id = @Id;
end
