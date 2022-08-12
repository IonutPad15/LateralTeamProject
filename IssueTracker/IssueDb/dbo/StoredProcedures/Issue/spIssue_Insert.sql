CREATE PROCEDURE [dbo].[spIssue_Insert]
	@Title NVARCHAR(50), 
	@Description NVARCHAR(max), 
	@Created datetime2(7), 
	@Updated datetime2(7), 
	@IssueTypeId INT,
	@ProjectId INT, 
	@UserAssignedId UNIQUEIDENTIFIER, 
	@PriorityId INT, 
	@StatusId INT, 
	@RoleId INT
AS
	insert into dbo.[Issue](Title, Description, Created, Updated, IssueTypeId, ProjectId, UserAssignedId, PriorityId, StatusId, RoleId)
	values(@Title , @Description, @Created, @Updated ,@IssueTypeId, @ProjectId, @UserAssignedId , @PriorityId, @StatusId, @RoleId )