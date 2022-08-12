CREATE PROCEDURE [dbo].[spParticipant_Update]
	@Id INT,
	@RoleId INT
AS
begin
	update dbo.[Participant]
	set RoleId = @RoleId
	where Id=@Id;
end
