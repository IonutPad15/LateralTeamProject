CREATE PROCEDURE [dbo].[spParticipant_Get]
	@Id int
AS
begin
	select Id, UserId, ProjectId, IssueId, RoleId, IsDeleted
	from dbo.[Participant]
	where @Id= Id AND IsDeleted = 0;
end

