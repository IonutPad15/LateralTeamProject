CREATE PROCEDURE [dbo].[spParticipant_Insert]
	@UserId UNIQUEIDENTIFIER,
	@ProjectId INT,
	@IssueId INT,
	@RoleId INT
AS
begin
	insert into dbo.[Participant] (UserId, ProjectId, IssueId, RoleId)
	values (@UserId, @ProjectId, @IssueId, @RoleId)
end
