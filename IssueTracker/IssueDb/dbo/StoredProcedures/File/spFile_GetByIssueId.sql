CREATE PROCEDURE [dbo].[spFile_GetByIssueId]
	@IssueId int
AS
begin
    if Exists(select Top 1 * from [dbo].[File] where FileIssueId = @IssueId)
	 SELECT * FROM [dbo].[File] where FileIssueId = @IssueId
    else
     THROW 51000, 'The record does not exist.', 1;
end
