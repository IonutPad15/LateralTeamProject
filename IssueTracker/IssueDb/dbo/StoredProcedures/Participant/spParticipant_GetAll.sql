CREATE PROCEDURE [dbo].[spParticipant_GetAll]
AS 
begin
	select * 
	from dbo.[Participant]
	where IsDeleted = 0;
end