CREATE PROCEDURE [dbo].[spParticipant_Get]
	@Id int
AS
begin
	select *
	from dbo.[Participant]
	inner join Project
	ON Project.Id = Participant.ProjectId
	where @Id= Participant.Id AND Participant.IsDeleted = 0;
end

