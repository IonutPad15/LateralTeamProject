CREATE PROCEDURE [dbo].[spParticipant_Update]
	@Id INT,
	@UserId UNIQUEIDENTIFIER,
	@ProjectId INT,
	@RoleId INT
AS
begin
	update dbo.[Participant]
	set UserId = @UserId, ProjectId = @ProjectId, RoleId = @RoleId
	where Id=@Id;
end
