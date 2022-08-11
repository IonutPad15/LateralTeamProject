CREATE PROCEDURE [dbo].[spParticipant_Get]
	@Id INT
AS
begin
	select * 
	from dbo.Participant
	where Id=@Id AND IsDeleted = 0
end

