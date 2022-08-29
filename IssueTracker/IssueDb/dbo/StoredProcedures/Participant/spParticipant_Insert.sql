CREATE PROCEDURE [dbo].[spParticipant_Insert]
	@UserId UNIQUEIDENTIFIER,
	@ProjectId INT,
	@RoleId INT
AS
begin
	insert into dbo.[Participant] (UserId, ProjectId, RoleId)
	values (@UserId, @ProjectId, @RoleId)
    select CAST(SCOPE_IDENTITY() as int);
end
