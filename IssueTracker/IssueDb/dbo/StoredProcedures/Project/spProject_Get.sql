CREATE PROCEDURE [dbo].[spProject_Get]
	@Id INT
AS
begin
	select *
	from dbo.[Project]
	Inner join Participant
	on Project.Id = Participant.ProjectId
	where Project.Id = @Id AND Project.IsDeleted = 0;
end	