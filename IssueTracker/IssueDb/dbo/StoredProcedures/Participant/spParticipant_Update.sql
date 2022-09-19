CREATE PROCEDURE [dbo].[spParticipant_Update]
	@Id INT,
	@RoleId INT
AS
begin
	IF EXISTS(SELECT * FROM dbo.[Participant] WHERE Id = @Id and IsDeleted = 0)
	begin
		update dbo.[Participant]
		set RoleId = @RoleId
		where Id=@Id;
	end
	ELSE
	begin;
		THROW 51000, 'The record does not exist.', 1;
	end
end
