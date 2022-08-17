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
end