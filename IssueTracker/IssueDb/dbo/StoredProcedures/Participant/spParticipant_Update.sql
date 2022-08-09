CREATE PROCEDURE [dbo].[spParticipant_Update]
	@Id INT,
	@UserId UNIQUEIDENTIFIER,
	@ProjectId INT,
	@IssueId INT,
	@RoleId INT
AS
begin
	update dbo.[Participant]
	set UserId = @UserId, ProjectId = @ProjectId, IssueId = @IssueId, RoleId = @RoleId
	where Id=@Id;
end
