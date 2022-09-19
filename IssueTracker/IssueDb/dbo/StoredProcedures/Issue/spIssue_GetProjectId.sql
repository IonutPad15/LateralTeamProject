CREATE PROCEDURE [dbo].[spIssue_GetProjectId]
	@Id INT
AS
begin
    if exists(select * from Issue where Id = @Id)
		select ProjectId
		from dbo.[Issue]
		where Issue.IsDeleted = 0 and Issue.Id = @Id;
	else
		THROW 51000, 'The record does not exist.', 1;
end
