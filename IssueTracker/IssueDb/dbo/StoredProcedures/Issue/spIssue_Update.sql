CREATE PROCEDURE [dbo].[spIssue_Update]
	@Id int,
	@Title varchar(50),
	@Description varchar(max),
	@ProjectId int,
	@PriorityId int,
	@Updated datetime2(7),
	@IssueTypeId int,
	@UserAssignedId uniqueidentifier,
	@StatusId int,
	@RoleId int
as
begin
	if exists(select * from Issue where Id = @Id)
		Update Issue 
		set 
		Title = @Title, 
		Description = @Description, 
		ProjectId = @ProjectId, 
		PriorityId = @PriorityId, 
		Updated = @Updated, 
		IssueTypeId = @IssueTypeId, 
		UserAssignedId = @UserAssignedId, 
		StatusId = @StatusId, 
		RoleId = @RoleId
		where Id = @Id;
	else
		THROW 5100, 'The record does not exist.', 1;
end