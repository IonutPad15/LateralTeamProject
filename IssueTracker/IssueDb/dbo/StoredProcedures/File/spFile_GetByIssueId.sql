CREATE PROCEDURE [dbo].[spFile_GetByIssueId]
	@IssueId int
AS
begin
	 SELECT *
     FROM [dbo].[File]
     where FileIssueId = @IssueId AND FileIsDeleted = 0
end
