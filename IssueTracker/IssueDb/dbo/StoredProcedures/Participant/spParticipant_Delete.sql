CREATE PROCEDURE [dbo].[spParticipant_Delete]
	@Id INT
AS
begin
	update dbo.[Participant]
	set IsDeleted = 1 /*TRUE*/
	where Id=@Id;
end